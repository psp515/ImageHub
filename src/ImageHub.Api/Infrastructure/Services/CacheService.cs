using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Concurrent;
using System.Text.Json;

namespace ImageHub.Api.Infrastructure.Services;

public class CacheService(IDistributedCache distributedCache) : ICacheService
{
    private static readonly ConcurrentDictionary<string, bool> CachedKeys = new();

    public async Task ExpireKey(string key, CancellationToken cancellationToken = default)
    {
        await distributedCache.RemoveAsync(key, cancellationToken);
        CachedKeys.TryRemove(key, out _);
    }

    public Task ExpireKeysByPrefix(string prefix, CancellationToken cancellationToken = default)
    {
        IEnumerable<Task> tasks = CachedKeys.Keys
            .Where(key => key.StartsWith(prefix))
            .Select(key => ExpireKey(key, cancellationToken));

        return Task.WhenAll(tasks);
    }

    public async Task<T?> Get<T>(string cacheKey, CancellationToken cancellationToken = default) where T : class
    {
        if (!CachedKeys.ContainsKey(cacheKey))
            return null;

        string? cachedData = await distributedCache
            .GetStringAsync(cacheKey, cancellationToken);

        if (cachedData is null)
            return null;

        T? value = JsonSerializer.Deserialize<T>(cachedData);

        return value;
    }

    public async Task Set<T>(string cacheKey, T data, TimeSpan expiration, CancellationToken cancellationToken = default) where T : class
    {
        var cacheData = JsonSerializer.Serialize(data);

        DistributedCacheEntryOptions? options = new()
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        await distributedCache.SetStringAsync(cacheKey, cacheData, options, cancellationToken);
        CachedKeys.TryAdd(cacheKey, false);
    }
}
