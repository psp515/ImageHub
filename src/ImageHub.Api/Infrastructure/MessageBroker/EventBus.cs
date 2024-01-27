using MassTransit;

namespace ImageHub.Api.Infrastructure.MessageBroker;

public sealed class EventBus(IPublishEndpoint publishEndpoint,
    ILogger<EventBus> logger) : IEventBus
{
    public Task Publish<T>(T data, CancellationToken cancellationToken = default)
        where T : class
    {
        logger.LogWarning("Event Type: {@RequestName}, Time: {@DateTimeUtc}, Event reported to event bus.",
            typeof(T).Name,
            DateTime.UtcNow);

        return publishEndpoint.Publish(data, cancellationToken);
    }
}
