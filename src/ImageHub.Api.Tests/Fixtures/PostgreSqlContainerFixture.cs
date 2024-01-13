using Microsoft.AspNetCore.Hosting;
using System.Runtime.InteropServices;
using Testcontainers.PostgreSql;

namespace ImageHub.Api.Tests;

public class PostgreSqlContainerFixture : IAsyncLifetime
{
    private const string UnixSocketAddr = "unix:/var/run/docker.sock";

    public PostgreSqlContainer Container { get; }

    public PostgreSqlContainerFixture()
    {
        var builder = new PostgreSqlBuilder()
                    .WithPortBinding(5432, true)
                    
                    .WithImage("postgres:latest")
                    .WithDatabase("imagehub-db")
                    .WithCleanUp(true);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            builder.WithDockerEndpoint(UnixSocketAddr);
        }

        Container = builder.Build();
    }

    public Task InitializeAsync()
    {
        return Container.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return Container.StopAsync();
    }

}
