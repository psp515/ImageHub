namespace ImageHub.Api.Abstractions.Messaging;

public interface ICacheQuery
{
    string Key { get; }
    TimeSpan? Expiration { get; }
}

public interface ICacheQuery<TResponse> : IQuery<TResponse>, ICacheQuery;
