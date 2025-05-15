using Shared.ValueObjects;

namespace Application.DTOs.Common;

public class LocalizedStringDto
{
    public string? En { get; init; }
    public string? Tr { get; init; }

    public Dictionary<string, string> Translations =>
        new()
        {
            ["en"] = En ?? string.Empty,
            ["tr"] = Tr ?? string.Empty
        };
    public LocalizedString ToValueObject()
    {
        var translations = new Dictionary<string, string>();

        if (!string.IsNullOrWhiteSpace(En)) translations["en"] = En;
        if (!string.IsNullOrWhiteSpace(Tr)) translations["tr"] = Tr;

        if (translations.Count == 0)
            throw new ArgumentException("LocalizedStringDto must contain at least one translation.");

        return LocalizedString.Create(translations);
    }
}