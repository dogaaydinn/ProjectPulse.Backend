using System.Globalization;

namespace Shared.Globalization;

public static class CultureExtensions
{
    public static IEnumerable<CultureInfo> GetFallbackCultures(this CultureInfo culture)
    {
        while (!culture.Equals(CultureInfo.InvariantCulture))
        {
            culture = culture.Parent;
            yield return culture;
        }
    }

    public static bool IsSupportedCulture(string cultureCode)
    {
        try
        {
            _ = CultureInfo.GetCultureInfo(cultureCode);
            return true;
        }
        catch (CultureNotFoundException)
        {
            return false;
        }
    }

    public static CultureInfo NormalizeCulture(string cultureCode)
    {
        try
        {
            return CultureInfo.GetCultureInfo(cultureCode);
        }
        catch (CultureNotFoundException ex)
        {
            throw new ArgumentException($"Invalid culture code: '{cultureCode}'", ex);
        }
    }
}