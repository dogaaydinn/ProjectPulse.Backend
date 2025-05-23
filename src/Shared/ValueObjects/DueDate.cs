using Shared.Results;
using Shared.Time;         

namespace Shared.ValueObjects;

public sealed class DueDate : ValueObject
{
    private DateTime Date { get; }

    private DueDate(DateTime date)
    {
        Date = date;
    }
    
    public static Result<DueDate> Create(
        DateTime date,
        IClock clock,
        IErrorFactory errors)
    {
        var today = clock.UtcNow.Date;
        if (date.Date >= today) return Result<DueDate>.Success(new DueDate(date.Date), errors);
        var err = errors.Validation(
                field: "DueDate",
                rule:  "Past",
                invalidValue: date.Date)
            .WithMetadata(StandardMetadata.Context, $"Today is {today:yyyy-MM-dd}");

        return Result<DueDate>.Failure(err, errors);

    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Date;
    }

    public override string ToString() => Date.ToString("yyyy-MM-dd");
}