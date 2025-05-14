using Domain.Core.Primitives.Enums.Base;
using Shared.Results;

namespace Domain.Core.Primitives.Enums.Converters;

public static class StructuredEnumConverter
{
    public static Result<TEnum> ConvertAsResult<TEnum>(string? name, string fieldName)
        where TEnum : StructuredEnum<TEnum, int>
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<TEnum>.Failure(ErrorFactory.EnumRequired(fieldName));

        var success = StructuredEnumHelper.TryFromName<TEnum>(name!, out var result);
        if (!success)
            return Result<TEnum>.Failure(ErrorFactory.EnumInvalid(fieldName, StructuredEnumHelper.AllValues<TEnum>().Select(e => e.Name)));

        return Result<TEnum>.Success(result!);
    }
}