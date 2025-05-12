using Shared.Exceptions;

namespace Domain.Core.ValueObjects;

public sealed class DateRange : ValueObject
{
    public DateTime Start { get; }
    public DateTime? End { get; }

    private DateRange(DateTime start, DateTime? end)
    {
        if (end.HasValue && end < start)
            throw new AppException("Validation.DateRange.Invalid", "End date must be after start date.");

        Start = start;
        End = end;
    }

    public static DateRange Create(DateTime start, DateTime? end)
        => new(start, end);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }

    public override string ToString() =>
        End is null ? $"{Start:yyyy-MM-dd}" : $"{Start:yyyy-MM-dd} → {End:yyyy-MM-dd}";
}