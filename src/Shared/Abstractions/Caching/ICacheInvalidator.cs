namespace Shared.Abstractions.Caching;

public interface ICacheInvalidator
{
    Task RemoveAsync(string key, CancellationToken ct = default);
    
    Task RemoveByPatternAsync(string pattern, CancellationToken ct = default);

    Task ClearAllAsync(CancellationToken ct = default);
}