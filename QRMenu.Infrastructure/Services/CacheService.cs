using Microsoft.Extensions.Caching.Distributed;
using QRMenu.Application.Interfaces;
using System.Text.Json;

namespace QRMenu.Infrastructure.Services;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var value = await _cache.GetStringAsync(key);
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null)
    {
        var options = new DistributedCacheEntryOptions();
        if (expirationTime.HasValue)
        {
            options.AbsoluteExpirationRelativeToNow = expirationTime.Value;
        }

        await _cache.SetStringAsync(key, JsonSerializer.Serialize(value), options);
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }

    public async Task<bool> ExistsAsync(string key)
    {
        return await _cache.GetAsync(key) != null;
    }
}