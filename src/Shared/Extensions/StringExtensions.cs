namespace Shared.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? value) 
        => string.IsNullOrEmpty(value);

    public static bool IsNullOrWhiteSpace(this string? value) 
        => string.IsNullOrWhiteSpace(value);

    public static string Truncate(this string value, int maxLength, string suffix = "...")
    {
        if (string.IsNullOrEmpty(value) || maxLength <= 0) 
            return string.Empty;

        if (value.Length <= maxLength) 
            return value;

        var allowed = maxLength - suffix.Length;
        return allowed > 0 
            ? value[..allowed] + suffix 
            : value[..maxLength];
    }

    public static string ToCamelCase(this string value)
    {
        if (string.IsNullOrEmpty(value) || char.IsLower(value[0]))
            return value;
        return char.ToLowerInvariant(value[0]) + value[1..];
    }
}