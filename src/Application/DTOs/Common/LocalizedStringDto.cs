using Shared.ValueObjects;

namespace Application.DTOs.Common;

public class LocalizedStringDto
{
    public string? En { get; init; }
    public string? Tr { get; init; }

    public Dictionary<string, string> Translations =>
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["en-US"] = En ?? string.Empty,
            ["tr-TR"] = Tr ?? string.Empty
        };

    public LocalizedString ToValueObject()
    {
        var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        if (!string.IsNullOrWhiteSpace(En)) dictionary["en-US"] = En;
        if (!string.IsNullOrWhiteSpace(Tr)) dictionary["tr-TR"] = Tr;

        return LocalizedString.Create(dictionary).Value;
    }
}