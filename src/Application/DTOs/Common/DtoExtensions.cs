using Shared.ValueObjects;

namespace Application.DTOs.Common;

public static class DtoExtensions
{
    public static LocalizedString ToLocalizedString(this LocalizedStringDto dto)
    {
        return LocalizedString.Create(dto.Translations);
    }

    public static DateRange ToDateRange(this DateRangeDto dto)
    {
        return DateRange.Create(dto.Start, dto.End);
    }
}