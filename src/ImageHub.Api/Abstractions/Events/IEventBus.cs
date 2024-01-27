namespace ImageHub.Api.Abstractions.Events;

public interface IEventBus
{
    Task Publish<T>(T data, CancellationToken cancellationToken = default)
        where T : class;
}