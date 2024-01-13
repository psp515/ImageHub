using ImageHub.Api.Infrastructure.Persistence;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;
using Testcontainers.PostgreSql;

namespace ImageHub.Api.Tests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private const string UnixSocketAddr = "unix:/var/run/docker.sock";

    private static PostgreSqlContainer _instance;

    private static PostgreSqlContainer _dbContainer
    {
        get
        {
            if (_instance is null)
            {
                var builder = new PostgreSqlBuilder()
                    .WithPortBinding(5432, true)
                    .WithImage("postgres:latest")
                    .WithPassword("postgres")
                    .WithUsername("postgres")
                    .WithDatabase("imagehub-db")
                    .WithCleanUp(true);

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    builder.WithDockerEndpoint(UnixSocketAddr);
                }

                return builder.Build();
            }

            return _instance;
        }
    }

    protected override async void ConfigureWebHost(IWebHostBuilder builder)
    {
        await _dbContainer.StartAsync();

        if (_dbContainer.State != DotNet.Testcontainers.Containers.TestcontainersStates.Running)
        {
            throw new InvalidOperationException("PostgreSQL container is not running.");
        }

        if (_dbContainer.Health != DotNet.Testcontainers.Containers.TestcontainersHealthStatus.Healthy)
        {
            throw new InvalidOperationException("PostgreSQL container is not healthy.");
        }

        builder.ConfigureTestServices(services =>
        {
            var descriptorType =
                typeof(DbContextOptions<ApplicationDbContext>);

            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == descriptorType);

            if (descriptor is not null)
                services.Remove(descriptor);

            var connectionString = $"Host={_dbContainer.Hostname};Port={_dbContainer.GetMappedPublicPort(5432)};Database=imagehub-db;Username=postgres;Password=postgres";

            Console.WriteLine(connectionString);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));
        });
    }

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }

}
