using System.Collections.ObjectModel;
using System.Globalization;
using Shared.Results;
using Shared.Globalization;

namespace Shared.ValueObjects;

public sealed class LocalizedString : ValueObject
{
    private const string DefaultCulture = "en-US";
    public IReadOnlyDictionary<string, string> Translations { get; }

    private LocalizedString(Dictionary<string, string> translations)
    {
        Translations = new ReadOnlyDictionary<string, string>(translations);
    }

    public static Result<LocalizedString> Create(
        IDictionary<string, string?>? translations,
        IErrorFactory errors)
    {
        if (translations is null || translations.Count == 0)
            return Result<LocalizedString>.Failure(
                errors.Validation("translations", "Required", null),
                errors);

        var normalized = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var (culture, text) in translations)
        {
            if (string.IsNullOrWhiteSpace(text))
                continue;

            if (!CultureExtensions.IsSupportedCulture(culture))
                return Result<LocalizedString>.Failure(
                    errors.Validation("translations", "InvalidCulture", culture),
                    errors);

            var name = CultureInfo.GetCultureInfo(culture).Name;
            normalized[name] = text.Trim();
        }

        if (normalized.Count == 0)
            return Result<LocalizedString>.Failure(
                errors.Validation("translations", "NoValidTranslations", null),
                errors);

        return Result<LocalizedString>.Success(
            new LocalizedString(normalized),
            errors);
    }

    private string GetValue(CultureInfo culture)
    {
        if (Translations.TryGetValue(culture.Name, out var v) || culture.GetFallbackCultures().Any(fallback => Translations.TryGetValue(fallback.Name, out v)) || Translations.TryGetValue(DefaultCulture, out v))
            return v ?? throw new InvalidOperationException();

        throw new InvalidOperationException($"No translation for {culture.Name}");
    }

    public string this[string culture] => GetValue(CultureInfo.GetCultureInfo(culture));

    public bool SupportsCulture(string culture) =>
        Translations.ContainsKey(culture);

    public bool IsEmpty() =>
        Translations.Values.All(string.IsNullOrWhiteSpace);

    public Dictionary<string, string> ToDictionary() =>
        new(Translations, StringComparer.OrdinalIgnoreCase);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        foreach (var pair in Translations.OrderBy(x => x.Key))
        {
            yield return pair.Key.ToLowerInvariant();
            yield return pair.Value;
        }
    }

    public override string ToString() =>
        GetValue(CultureInfo.CurrentCulture);
}