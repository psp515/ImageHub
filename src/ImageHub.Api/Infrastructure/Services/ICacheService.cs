namespace ImageHub.Api.Infrastructure.Services;

public interface ICacheService
{
    Task<T> GetOrCreateAsync<T>(string key,
        Func<CancellationToken, Task<T>> factory,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default);

    Task ExpireKey<T>(string key);
}
