namespace Shared.Extensions;

public static class EnumExtensions
{
    public static TEnum ParseOrDefault<TEnum>(this string value, TEnum defaultValue)
        where TEnum : struct, Enum
    {
        return Enum.TryParse<TEnum>(value, ignoreCase: true, out var result) 
            ? result 
            : defaultValue;
    }

    public static IReadOnlyDictionary<int, string> ToDictionary<TEnum>()
        where TEnum : struct, Enum
    {
        return Enum.GetValues<TEnum>()
            .ToDictionary(e => Convert.ToInt32(e), e => e.ToString());
    }

    public static IEnumerable<string> GetNames<TEnum>() 
        where TEnum : struct, Enum
        => Enum.GetNames<TEnum>();

    public static IEnumerable<TEnum> GetValues<TEnum>() 
        where TEnum : struct, Enum
        => Enum.GetValues<TEnum>().Cast<TEnum>();
}