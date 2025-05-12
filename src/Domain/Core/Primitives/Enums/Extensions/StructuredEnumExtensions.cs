using Domain.Core.Primitives.Enums.Base;

namespace Domain.Core.Primitives.Enums.Extensions;

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
            if (!item.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) continue;
            result = item;
            return true;
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
            if (!item.Value.Equals(value)) continue;
            result = item;
            return true;
        }
        return false;
    }
}