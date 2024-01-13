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

    private static PostgreSqlContainer _dbContainer
    {
        get
        {
            var builder = new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .WithPassword("postgres")
                .WithUsername("postgres")
                .WithDatabase("imagehub");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                builder.WithDockerEndpoint(UnixSocketAddr);
            }

            return builder.Build();
        }
    }

    protected override async void ConfigureWebHost(IWebHostBuilder builder)
    {
        await _dbContainer.StartAsync();

        builder.ConfigureTestServices(services =>
        {
            var descriptorType =
                typeof(DbContextOptions<ApplicationDbContext>);

            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == descriptorType);

            if (descriptor is not null)
                services.Remove(descriptor);

            var connection = _dbContainer.GetConnectionString();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connection));
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
