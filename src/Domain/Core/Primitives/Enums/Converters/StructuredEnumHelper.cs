using System.Reflection;
using Domain.Core.Primitives.Enums.Base;

namespace Domain.Core.Primitives.Enums.Converters;

public static class StructuredEnumHelper
{
    public static TEnum FromName<TEnum>(string name)
        where TEnum : StructuredEnum<TEnum, int>
    {
        var method = typeof(TEnum).GetMethod("FromName", BindingFlags.Public | BindingFlags.Static);
        return (TEnum)method!.Invoke(null, [name])!;
    }

    public static bool TryFromName<TEnum>(string name, out TEnum? result)
        where TEnum : StructuredEnum<TEnum, int>
    {
        var method = typeof(TEnum).GetMethod("TryFromName", BindingFlags.Public | BindingFlags.Static);
        var parameters = new object?[] { name, null };

        var success = (bool)method!.Invoke(null, parameters)!;
        result = (TEnum?)parameters[1];

        return success;
    }

    public static TEnum FromValue<TEnum>(int value)
        where TEnum : StructuredEnum<TEnum, int>
    {
        var method = typeof(TEnum).GetMethod("FromValue", BindingFlags.Public | BindingFlags.Static);
        return (TEnum)method!.Invoke(null, [value])!;
    }

    public static bool TryFromValue<TEnum>(int value, out TEnum? result)
        where TEnum : StructuredEnum<TEnum, int>
    {
        var method = typeof(TEnum).GetMethod("TryFromValue", BindingFlags.Public | BindingFlags.Static);
        var parameters = new object?[] { value, null };

        var success = (bool)method!.Invoke(null, parameters)!;
        result = (TEnum?)parameters[1];

        return success;
    }
    
    public static IEnumerable<TEnum> AllValues<TEnum>()
        where TEnum : StructuredEnum<TEnum, int>
    {
        var method = typeof(TEnum).GetMethod("AllValues", BindingFlags.Public | BindingFlags.Static);
        return (IEnumerable<TEnum>)method!.Invoke(null, null)!;
    }
}
