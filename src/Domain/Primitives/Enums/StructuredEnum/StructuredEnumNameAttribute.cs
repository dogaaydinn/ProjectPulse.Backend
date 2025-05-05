using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Domain.Primitives.Enums.StructuredEnum;

[AttributeUsage(AttributeTargets.Property)]
public class StructuredEnumNameAttribute : ValidationAttribute
{
    private readonly Type _enumType;
    private readonly bool _allowNull;

    public StructuredEnumNameAttribute(Type enumType, bool allowNull = false)
    {
        _enumType = enumType;
        _allowNull = allowNull;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null)
            return _allowNull ? ValidationResult.Success :
                new ValidationResult($"{validationContext.DisplayName} cannot be null");

        if (value is not string strValue)
            return new ValidationResult($"{validationContext.DisplayName} must be a string");

        var method = _enumType.GetMethod("FromName", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);

        if (method == null)
            return new ValidationResult($"{_enumType.Name} must have a FromName method");

        try
        {
            _ = method.Invoke(null, new object[] { strValue });
            return ValidationResult.Success;
        }
        catch
        {
            return new ValidationResult($"'{strValue}' is not a valid name for {_enumType.Name}");
        }
    }
}