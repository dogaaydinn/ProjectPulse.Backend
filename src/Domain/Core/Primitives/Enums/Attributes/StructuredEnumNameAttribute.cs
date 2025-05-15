using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Domain.Core.Primitives.Enums.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class StructuredEnumNameAttribute(Type enumType, bool allowNull = true) : ValidationAttribute
{
    private static readonly ConcurrentDictionary<Type, MethodInfo> TryParseCache = new();

    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        if (value is null)
            return allowNull ? ValidationResult.Success : new ValidationResult($"{context.DisplayName} cannot be null.");

        if (value is not string name)
            return new ValidationResult($"{context.DisplayName} must be a string.");

        var method = TryParseCache.GetOrAdd(enumType, t => t.GetMethod("TryFromName", BindingFlags.Static | BindingFlags.Public)!);
        var parameters = new object?[] { name, null };
        var success = (bool)method.Invoke(null, parameters)!;

        return success
            ? ValidationResult.Success
            : new ValidationResult($"{context.DisplayName} must be one of the defined values in {enumType.Name}.");
    }
}