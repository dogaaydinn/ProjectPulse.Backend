using Domain.Core.Primitives.Enums.Base;
using Domain.Core.Primitives.Enums.Converters;
using Shared.Constants;
using Shared.Results;

namespace Application.Common.Validation;

public static class EnumValidationExtensions
{
    public static Result<TEnum> ConvertAsResult<TEnum>(this string? name)
        where TEnum : StructuredEnum<TEnum, int>
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<TEnum>.Failure(ErrorFactory.EnumRequired(typeof(TEnum).Name));

        var success = StructuredEnumHelper.TryFromName<TEnum>(name, out var result);
        return !success ? Result<TEnum>.Failure(ErrorFactory.EnumInvalid(typeof(TEnum).Name, StructuredEnumHelper.AllValues<TEnum>().Select(e => e.Name))) : Result<TEnum>.Success(result!);
    }

    public static void IfInvalidEnum<TEnum>(this ValidationResult result, string? name, string propertyName)
        where TEnum : StructuredEnum<TEnum, int>
    {
        if (string.IsNullOrWhiteSpace(name) || !StructuredEnumHelper.TryFromName<TEnum>(name, out _))
        {
            result.AddError(propertyName, string.Format(ValidationMessages.Enum.InvalidName, name));
        }
    }
}