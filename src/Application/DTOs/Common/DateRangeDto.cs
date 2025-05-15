using Shared.ValueObjects;

namespace Application.DTOs.Common;

public abstract class DateRangeDto
{
    public DateTime Start { get; set; }
    public DateTime? End { get; set; }

    public DateRange ToValueObject()
        => DateRange.Create(Start, End);
}