using Application.DTOs.Common;
using Shared.ValueObjects;

namespace Application.Common.Mapping.Mappers;

public static class LocalizedStringMapper
{
    public static LocalizedString ToValueObject(LocalizedStringDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var translations = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        if (!string.IsNullOrWhiteSpace(dto.En)) translations["en-US"] = dto.En;
        if (!string.IsNullOrWhiteSpace(dto.Tr)) translations["tr-TR"] = dto.Tr;

        return LocalizedString.Create(translations).Value;
    }

    public static LocalizedStringDto ToDto(LocalizedString localized)
    {
        ArgumentNullException.ThrowIfNull(localized);

        localized.Translations.TryGetValue("en-US", out var en);
        localized.Translations.TryGetValue("tr-TR", out var tr);

        return new LocalizedStringDto
        {
            En = en,
            Tr = tr
        };
    }
}