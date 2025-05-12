using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Domain.Core.Primitives.Enums.Interfaces;

namespace Domain.Core.Primitives.Enums.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class StructuredEnumNameAttribute : ValidationAttribute
{
    private static readonly ConcurrentDictionary<Type, MethodInfo> _tryParseMethods = new();
    private readonly Type _enumType;
    private readonly bool _allowNull;

    public StructuredEnumNameAttribute(Type enumType, bool allowNull = true)
    {
        _enumType = enumType;
        _allowNull = allowNull;

        if (!typeof(IStructuredEnum).IsAssignableFrom(_enumType))
            throw new ArgumentException("StructuredEnumNameAttribute can only be used with StructuredEnum types.");
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return _allowNull ? ValidationResult.Success : new ValidationResult($"{validationContext.DisplayName} cannot be null.");

        if (value is not string stringValue)
            return new ValidationResult($"{validationContext.DisplayName} must be a string.");

        var tryParseMethod = _tryParseMethods.GetOrAdd(_enumType, 
            t => t.GetMethod("TryFromName", BindingFlags.Public | BindingFlags.Static) 
                 ?? throw new InvalidOperationException($"TryFromName method not found on {t.Name}."));

        object[] parameters = { stringValue, null! };
        bool isValid = (bool)tryParseMethod.Invoke(null, parameters)!;

        return isValid
            ? ValidationResult.Success
            : new ValidationResult($"{validationContext.DisplayName} must be one of the defined values in {_enumType.Name}.");
    }
}