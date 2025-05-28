using Shared.Results;

namespace Shared.ValueObjects;

public sealed class DateRange : ValueObject
{
    public DateTime Start { get; }
    public DateTime? End { get; }

    private DateRange(DateTime start, DateTime? end)
    {
        Start = start;
        End = end;
    }

    public static Result<DateRange> Create(
        DateTime start,
        DateTime? end,
        IErrorFactory errors)
    {
        if (!end.HasValue || !(end < start)) return Result<DateRange>.Success(new DateRange(start, end), errors);
        var err = errors.Create(
            code: "DateRange.Invalid",                
            args: [start, end],
            metadata: new Dictionary<string, object>
            {
                ["Start"] = start,
                ["End"]   = end
            },
            severity: ErrorSeverity.Validation,     
            category: ErrorCategory.Validation);   

        return Result<DateRange>.Failure(err, errors);

    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
    public bool IsEmpty() => Start == default && End == null;

    public override string ToString() =>
        End is null ? $"{Start:yyyy-MM-dd}" : $"{Start:yyyy-MM-dd} → {End:yyyy-MM-dd}";
}