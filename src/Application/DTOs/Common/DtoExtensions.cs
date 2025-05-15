using Shared.ValueObjects;

namespace Application.DTOs.Common;

public static class DtoExtensions
{
    public static LocalizedString ToLocalizedString(this LocalizedStringDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        return LocalizedString.Create(dto.Translations);
    }

    public static DateRange ToDateRange(this DateRangeDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        return DateRange.Create(dto.Start, dto.End);
    }
}