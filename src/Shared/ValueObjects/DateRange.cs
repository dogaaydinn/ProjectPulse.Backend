using Shared.Exceptions;
using Shared.Results.Errors;

namespace Shared.ValueObjects;

public sealed class DateRange : ValueObject
{
    public DateTime Start { get; }
    public DateTime? End { get; }

    public DateRange(DateTime start, DateTime? end)
    {
        if (end < start)
            throw new AppException(ProjectErrors.NameRequired());

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
    public bool IsEmpty() => Start == default && End == null;

    public override string ToString() =>
        End is null ? $"{Start:yyyy-MM-dd}" : $"{Start:yyyy-MM-dd} → {End:yyyy-MM-dd}";
}