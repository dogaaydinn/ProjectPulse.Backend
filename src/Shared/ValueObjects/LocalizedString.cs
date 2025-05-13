using Shared.Constants;
using Shared.Exceptions;

namespace Shared.ValueObjects;

public sealed class LocalizedString : ValueObject
{
    private const string DefaultCulture = "en";

    private Dictionary<string, string> Translations { get; }

    private LocalizedString(Dictionary<string, string> translations)
    {
        Translations = translations;
    }

    public static LocalizedString Create(Dictionary<string, string> translations)
    {
        if (translations is null || translations.Count == 0)
            throw new AppException(ErrorCodes.Validation, ValidationMessages.Common.LocalizedStringRequired);

        return new LocalizedString(translations);
    }

    private string? this[string culture] => 
        Translations.TryGetValue(culture, out var value) 
            ? value 
            : Translations.GetValueOrDefault(DefaultCulture);

    public string GetValue(string culture) => 
        this[culture] ?? throw new AppException(
            ErrorCodes.Validation, 
            $"No translation found for culture '{culture}' and no fallback available."
        );

    public string? TryGetValue(string culture) => this[culture];

    public bool IsEmpty() => Translations.Values.All(string.IsNullOrWhiteSpace);

    public override string ToString() => this[DefaultCulture] ?? string.Empty;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        foreach (var pair in Translations.OrderBy(x => x.Key))
        {
            yield return pair.Key;
            yield return pair.Value;
        }
    }
}