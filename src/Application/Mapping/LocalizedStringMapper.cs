using Application.DTOs.Common;
using Shared.ValueObjects;

namespace Application.Mapping;

public static class LocalizedStringMapper
{
    public static LocalizedString ToValueObject(LocalizedStringDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var translations = new Dictionary<string, string>();

        if (!string.IsNullOrWhiteSpace(dto.En))
            translations["en"] = dto.En;

        if (!string.IsNullOrWhiteSpace(dto.Tr))
            translations["tr"] = dto.Tr;

        if (translations.Count == 0)
            throw new ArgumentException("LocalizedStringDto must contain at least one translation.");

        return LocalizedString.Create(translations);
    }

    public static LocalizedStringDto ToDto(LocalizedString localized)
    {
        ArgumentNullException.ThrowIfNull(localized);

        return new LocalizedStringDto
        {
            Translations = localized.Translations.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
        };
}