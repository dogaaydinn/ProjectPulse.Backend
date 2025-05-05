namespace Domain.Primitives.Enums.StructuredEnum;

public static class StructuredEnumExtensions
{
    public static bool TryFromName<TEnum, TValue>(this IEnumerable<TEnum> list, string name, out TEnum result)
        where TEnum : StructuredEnum<TEnum, TValue>
        where TValue : IComparable<TValue>, IEquatable<TValue>
    {
        result = default;
        if (string.IsNullOrWhiteSpace(name)) return false;

        foreach (var item in list)
        {
            if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                result = item;
                return true;
            }
        }
        return false;
    }

    public static bool TryFromValue<TEnum, TValue>(this IEnumerable<TEnum> list, TValue value, out TEnum result)
        where TEnum : StructuredEnum<TEnum, TValue>
        where TValue : IComparable<TValue>, IEquatable<TValue>
    {
        result = default;
        foreach (var item in list)
        {
            if (item.Value.Equals(value))
            {
                result = item;
                return true;
            }
        }
        return false;
    }
}