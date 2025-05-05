using System.Reflection;

namespace Domain.Primitives.Enums.StructuredEnum;

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