
namespace ImageHub.Api.Infrastructure.Services;

public class CacheService : ICacheService
{
    public Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
