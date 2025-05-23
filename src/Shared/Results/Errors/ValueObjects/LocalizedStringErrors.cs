using Shared.Constants;

namespace Shared.Results.Errors.ValueObjects;

public static class LocalizedStringErrorExtensions
{
    public static Error Required(this IErrorFactory factory) =>
        factory.Create(code: ErrorCodes.LocalizedString.Required);

    public static Error NoValidTranslations(this IErrorFactory factory) =>
        factory.Create(code: ErrorCodes.LocalizedString.NoValidTranslations);

    public static Error InvalidCulture(this IErrorFactory factory, string culture) =>
        factory.Create(
            code: ErrorCodes.LocalizedString.InvalidCulture,
            args: new object[] { culture },
            metadata: new Dictionary<string, object>
            {
                [StandardMetadata.Field]   = "LocalizedString",
                [StandardMetadata.Culture] = culture
            });

    public static Error MissingCulture(this IErrorFactory factory, string cultureCode) =>
        factory.Create(
            code: ErrorCodes.LocalizedString.MissingCulture,
            args: new object[] { cultureCode },
            metadata: new Dictionary<string, object>
            {
                [StandardMetadata.Culture] = cultureCode
            });
}