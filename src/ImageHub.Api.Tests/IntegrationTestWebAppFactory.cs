using ImageHub.Api.Features.Images.AddImage;
using ImageHub.Api.Features.Images.Repositories;
using ImageHub.Api.Infrastructure.Persistence;
using ImageHub.Api.Tests.Fixtures;
using ImageHub.Api.Tests.Mocks;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ImageHub.Api.Tests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private static readonly PostgreSqlContainerFixture postgresFixture = new();
    private static readonly RabbitMqContainerFixture rabbitMqFixture = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            ReplacePostgreSql(services);
            ReplaceImageStore(services);
            ReplaceRabbitMqBus(services);
        });
    }

    private static void ReplaceRabbitMqBus(IServiceCollection services)
    {
        var connectionString = rabbitMqFixture.Container.GetConnectionString();

        services.AddMassTransitTestHarness(bus =>
        {
            bus.SetKebabCaseEndpointNameFormatter();

            bus.AddConsumer<AddImageEventConsumer>();

            bus.UsingRabbitMq((context, config) =>
            {
                config.ReceiveEndpoint("addimage", endpoint =>
                {
                    endpoint.ConfigureConsumer<AddImageEventConsumer>(context);
                });

                config.Host(new Uri(connectionString), host =>
                {
                    host.Username(rabbitMqFixture.Username);
                    host.Password(rabbitMqFixture.Password);
                });
            });
        });
    }
    private static void ReplacePostgreSql(IServiceCollection services)
    {
        var descriptorType =
                typeof(DbContextOptions<ApplicationDbContext>);

        var descriptor = services
            .SingleOrDefault(s => s.ServiceType == descriptorType);

        if (descriptor is not null)
            services.Remove(descriptor);

        var connectionString = postgresFixture.Container.GetConnectionString();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
    }
    private static void ReplaceImageStore(IServiceCollection services)
    {
        var imageStoreType = typeof(IImageStoreRepository);

        var descriptor = services
            .SingleOrDefault(s => s.ServiceType == imageStoreType);

        if (descriptor is not null)
            services.Remove(descriptor);

        services.AddSingleton<IImageStoreRepository, ImageStoreMock>();
    }

    public async Task InitializeAsync()
    {
        await postgresFixture.InitializeAsync();
        await rabbitMqFixture.InitializeAsync();

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.EnsureCreatedAsync();

        if (postgresFixture.Container.State != DotNet.Testcontainers.Containers.TestcontainersStates.Running)
        {
            throw new InvalidOperationException("PostgreSQL container is not running.");
        }

        if (rabbitMqFixture.Container.State != DotNet.Testcontainers.Containers.TestcontainersStates.Running)
        {
            throw new InvalidOperationException("RqbbitMq container is not running.");
        }
    }

    public new Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}
