namespace Shared.Extensions;

public static class EnumExtensions
{
    public static TEnum ParseOrDefault<TEnum>(this string value, TEnum defaultValue) 
        where TEnum : struct, Enum
    {
        return Enum.TryParse<TEnum>(value, true, out var result) ? result : defaultValue;
    }

    public static IReadOnlyDictionary<int, string> ToDictionary<TEnum>() 
        where TEnum : struct, Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .ToDictionary(e => Convert.ToInt32(e), e => e.ToString());
    }
}