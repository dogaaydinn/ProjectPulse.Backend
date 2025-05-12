using System.Reflection;

namespace Domain.Core.Primitives.Enums.Helpers;

public static class TypeExtensions
{
    public static IEnumerable<TFieldType> GetFieldsOfType<TFieldType>(this Type type)
    {
        return type
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => f.FieldType == typeof(TFieldType))
            .Select(f => (TFieldType)f.GetValue(null));
    }
}