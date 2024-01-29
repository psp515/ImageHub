using Testcontainers.RabbitMq;

namespace ImageHub.Api.Tests.Fixtures;

public class RabbitMqContainerFixture
{
    public RabbitMqContainer Container { get; }

    public string Username => "testguest";
    public string Password => "testguest";

    public RabbitMqContainerFixture()
    {
        var builder = new RabbitMqBuilder()
            .WithImage("rabbitmq:latest")
            .WithPortBinding(15672, true)
            .WithPassword(Password)
            .WithUsername(Username);

        Container = builder.Build();
    }

    public async Task InitializeAsync()
    {
        await Container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await Container.StopAsync();
    }
}
