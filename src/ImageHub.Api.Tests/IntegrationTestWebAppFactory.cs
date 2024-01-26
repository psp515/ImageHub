using ImageHub.Api.Features.Images.Repositories;
using ImageHub.Api.Infrastructure.Persistence;
using ImageHub.Api.Tests.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ImageHub.Api.Tests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private static readonly PostgreSqlContainerFixture postgresFixture = new();
    public string ConnectionString { get; private set; } = string.Empty;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptorType =
                typeof(DbContextOptions<ApplicationDbContext>);

            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == descriptorType);

            if (descriptor is not null)
                services.Remove(descriptor);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(ConnectionString));

            var imageStoreType = typeof(IImageStoreRepository);

            descriptor = services
                .SingleOrDefault(s => s.ServiceType == imageStoreType);

            if (descriptor is not null)
                services.Remove(descriptor);

            services.AddSingleton<IImageStoreRepository, ImageStoreMock>();
        });
    }

    public async Task InitializeAsync()
    {
        await postgresFixture.InitializeAsync();
        ConnectionString = postgresFixture.Container.GetConnectionString();

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.EnsureCreatedAsync();

        if (postgresFixture.Container.State != DotNet.Testcontainers.Containers.TestcontainersStates.Running)
        {
            throw new InvalidOperationException("PostgreSQL container is not running.");
        }
    }

    public new Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}
