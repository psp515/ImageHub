namespace ImageHub.Api.Infrastructure.Services;

public interface ICacheService
{
    Task<T?> Get<T>(string cacheKey, CancellationToken cancellationToken = default)
        where T : class;
    Task Set<T>(string cacheKey, T data, TimeSpan expiration, CancellationToken cancellationToken = default)
        where T : class;

    Task ExpireKey(string key, CancellationToken cancellationToken = default);
    Task ExpireKeysByPrefix(string prefix, CancellationToken cancellationToken = default);
}
