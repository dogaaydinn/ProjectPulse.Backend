using System.Collections.Concurrent;
using System.Globalization;

namespace Shared.Globalization;

public static class CultureExtensions
{
    private static readonly ConcurrentDictionary<string, CultureInfo?> _cache 
        = new(StringComparer.OrdinalIgnoreCase);

    public static IEnumerable<CultureInfo> GetFallbackCultures(this CultureInfo culture)
    {
        while (true)
        {
            var parent = culture.Parent;
            yield return parent;
            if (parent.Equals(CultureInfo.InvariantCulture))
                yield break;
            culture = parent;
        }
    }

    public static bool IsSupportedCulture(string cultureCode)
    {
        return TryNormalizeCulture(cultureCode, out _);
    }

    public static CultureInfo NormalizeCulture(string cultureCode)
    {
        if (!TryNormalizeCulture(cultureCode, out var culture))
            throw new ArgumentException($"Invalid culture code: '{cultureCode}'", nameof(cultureCode));
        return culture;
    }

    private static bool TryNormalizeCulture(string cultureCode, out CultureInfo culture)
    {
        if (string.IsNullOrWhiteSpace(cultureCode))
        {
            culture = CultureInfo.InvariantCulture;
            return false;
        }

        if (_cache.TryGetValue(cultureCode, out var cached))
        {
            if (cached is null)
            {
                culture = CultureInfo.InvariantCulture;
                return false;
            }
            culture = cached;
            return true;
        }

        try
        {
            var info = CultureInfo.GetCultureInfo(cultureCode);
            _cache[cultureCode] = info;
            culture = info;
            return true;
        }
        catch (CultureNotFoundException)
        {
            _cache[cultureCode] = null;
            culture = CultureInfo.InvariantCulture;
            return false;
        }
    }
}