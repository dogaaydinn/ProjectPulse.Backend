namespace Domain.ValueObjects;

public sealed class DateRange
{
    public DateTime Start { get; }
    public DateTime End { get; }

    private DateRange(DateTime start, DateTime end)
    {
        if (end < start)
            throw new ArgumentException("End date must be after start date.");

        Start = start;
        End = end;
    }

    public static DateRange Create(DateTime start, DateTime end) => new(start, end);

    public bool Overlaps(DateRange other)
    {
        return Start <= other.End && End >= other.Start;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not DateRange other) return false;
        return Start == other.Start && End == other.End;
    }

    public override int GetHashCode() => HashCode.Combine(Start, End);
}