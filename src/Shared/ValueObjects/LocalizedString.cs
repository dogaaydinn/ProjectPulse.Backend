using System.Collections.ObjectModel;
using System.Globalization;
using Shared.Exceptions;
using Shared.Globalization;
using Shared.Results;

namespace Shared.ValueObjects;

public sealed class LocalizedString : ValueObject
{
    private const string DefaultCulture = "en-US";

    public IReadOnlyDictionary<string, string> Translations { get; }

    private LocalizedString(Dictionary<string, string> translations)
    {
        Translations = new ReadOnlyDictionary<string, string>(translations);
    }

    public static Result<LocalizedString> Create(Dictionary<string, string> translations)
    {
        if (translations is null || translations.Count == 0)
            return Result.Failure<LocalizedString>(ErrorFactory.LocalizedString.Required());

        var normalized = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var (culture, value) in translations)
        {
            if (string.IsNullOrWhiteSpace(value))
                continue;

            if (!CultureExtensions.IsSupportedCulture(culture))
                return Result.Failure<LocalizedString>(ErrorFactory.LocalizedString.InvalidCulture(culture));

            normalized[CultureInfo.GetCultureInfo(culture).Name] = value.Trim();
        }

        if (normalized.Count == 0)
            return Result.Failure<LocalizedString>(ErrorFactory.LocalizedString.NoValidTranslations());

        return Result.Success(new LocalizedString(normalized));
    }

    public string GetValue(CultureInfo culture)
    {
        if (Translations.TryGetValue(culture.Name, out var value))
            return value;

        foreach (var fallback in culture.GetFallbackCultures())
        {
            if (Translations.TryGetValue(fallback.Name, out value))
                return value;
        }

        if (Translations.TryGetValue(DefaultCulture, out value))
            return value;

        throw new LocalizationException(culture.Name);
    }

    public string this[string culture] => GetValue(CultureInfo.GetCultureInfo(culture));

    public bool SupportsCulture(string culture) =>
        Translations.ContainsKey(culture);

    public bool IsEmpty() =>
        Translations.Values.All(string.IsNullOrWhiteSpace);

    public Dictionary<string, string> ToDictionary() =>
        new(Translations, StringComparer.OrdinalIgnoreCase);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        foreach (var pair in Translations.OrderBy(x => x.Key))
        {
            yield return pair.Key.ToLowerInvariant();
            yield return pair.Value;
        }
    }
}
