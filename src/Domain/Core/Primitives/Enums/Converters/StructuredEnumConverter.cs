using Domain.Core.Primitives.Enums.Base;
using Shared.Results;
using Shared.Results.Errors;

namespace Domain.Core.Primitives.Enums.Converters;

public static class StructuredEnumConverter
{
    public static TEnum ConvertOrThrow<TEnum>(string name, string fieldName)
        where TEnum : StructuredEnum<TEnum, int>
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(EnumErrors.Required(fieldName));

        if (TEnum.TryFromName(name, out var result))
            return result!;

        var validOptions = TEnum.List.Select(x => x.Name);
        throw new ArgumentException(EnumErrors.Invalid(fieldName, validOptions));
    }

    public static bool TryConvert<TEnum>(string name, out TEnum? result)
        where TEnum : StructuredEnum<TEnum, int>
    {
        return TEnum.TryFromName(name, out result);
    }

    public static Result<TEnum> ConvertAsResult<TEnum>(string name, string fieldName)
        where TEnum : StructuredEnum<TEnum, int>
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<TEnum>.Failure(ErrorFactory.EnumRequired(fieldName));

        if (TEnum.TryFromName(name, out var result))
            return Result<TEnum>.Success(result!);

        var validOptions = TEnum.List.Select(x => x.Name);
        return Result<TEnum>.Failure(ErrorFactory.EnumInvalid(fieldName, validOptions));
    }
}