using Shared.Exceptions;

namespace Domain.ValueObjects;

public class DateRange
{
    public DateTime Start { get; }
    public DateTime End { get; }

    protected DateRange() { }

    private DateRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public static DateRange Create(DateTime start, DateTime end)
    {
        if (end < start)
            throw new AppException("Validation.DateRange.Invalid", "End date cannot be earlier than start date.");

        return new DateRange(start, end);
    }

    public bool Overlaps(DateRange other)
        => Start < other.End && End > other.Start;

    public bool Contains(DateTime date)
        => Start <= date && date <= End;

    public override string ToString()
        => $"{Start:yyyy-MM-dd} - {End:yyyy-MM-dd}";

    public override bool Equals(object? obj)
        => obj is DateRange range && Start == range.Start && End == range.End;

    public override int GetHashCode()
        => HashCode.Combine(Start, End);
}