using Domain.Core.Primitives.Enums.Base;
using Shared.Results;

namespace Domain.Core.Primitives.Enums.Converters;

public class StructuredEnumConverter
{
    private readonly IErrorFactory _errors;

    public StructuredEnumConverter(IErrorFactory errors)
    {
        _errors = errors;
    }

    public Result<TEnum> ConvertAsResult<TEnum>(string? name, string fieldName)
        where TEnum : StructuredEnum<TEnum, int>
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            var err = _errors.Required(fieldName);
            return Result<TEnum>.Failure(err, _errors);
        }

        var ok = StructuredEnumHelper.TryFromName<TEnum>(name!, out var enumVal);
        if (ok) return Result<TEnum>.Success(enumVal!, _errors);
        {
            var validValues = StructuredEnumHelper.AllValues<TEnum>().Select(e => e.Name);
            var err = _errors.Invalid(fieldName, validValues);
            return Result<TEnum>.Failure(err, _errors);
        }
        
    }
}