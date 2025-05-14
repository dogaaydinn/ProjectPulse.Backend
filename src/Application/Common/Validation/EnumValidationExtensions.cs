using Domain.Core.Primitives.Enums.Base;
using Domain.Core.Primitives.Enums.Converters;
using Shared.Results;

namespace Application.Common.Validation;

public static class EnumValidationExtensions
{
    public static Result<TEnum> ConvertAsResult<TEnum>(this string? name, Func<Error> errorFactory)
        where TEnum : StructuredEnum<TEnum, int>
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<TEnum>.Failure(errorFactory());

        var success = StructuredEnumHelper.TryFromName<TEnum>(name, out var result);
        return !success 
            ? Result<TEnum>.Failure(errorFactory()) 
            : Result<TEnum>.Success(result!);
    }

    public static void IfInvalidEnum<TEnum>(this ValidationResult result, string? name, Func<Error> errorFactory)
        where TEnum : StructuredEnum<TEnum, int>
    {
        if (string.IsNullOrWhiteSpace(name) || !StructuredEnumHelper.TryFromName<TEnum>(name, out _))
        {
            result.Add(errorFactory());
        }
    }
}