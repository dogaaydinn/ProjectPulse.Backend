using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Abstractions.Caching;
using Shared.Services;
using Shared.Time;
using StackExchange.Redis;

namespace Infrastructure.Caching;

public sealed class RedisCacheService : ICacheService
{
    private readonly IDatabase _db;
    private readonly JsonSerializerOptions _options;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
        _options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
    {
        var value = await _db.StringGetAsync(key);
        return value.HasValue ? JsonSerializer.Deserialize<T>(value!, _options) : default;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken ct = default)
    {
        var serialized = JsonSerializer.Serialize(value, _options);
        await _db.StringSetAsync(key, serialized, expiry);
    }

    public async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, 
        TimeSpan? expiry = null, CancellationToken ct = default)
    {
        var value = await GetAsync<T>(key, ct);
        if (value != null) return value;
    
        // RedLock ile distributed lock
        await using var redLock = await _redLockFactory.CreateLockAsync(
            $"lock:{key}", TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10));
        
        if (!redLock.IsAcquired)
            throw new TimeoutException("Could not acquire lock for cache key");
        
        value = await factory();
        await SetAsync(key, value, expiry, ct);
        return value;
    }
    public async Task RemoveAsync(string key, CancellationToken ct = default) => await _db.KeyDeleteAsync(key);

    public async Task RefreshAsync(string key, CancellationToken ct = default) => await _db.KeyTouchAsync(key);
}