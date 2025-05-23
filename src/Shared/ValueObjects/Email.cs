using System.Text.RegularExpressions;
using Shared.Results;

namespace Shared.ValueObjects;

public sealed partial class Email : ValueObject
{
    public string Value { get; }
    public string Domain => Value.Split('@')[1];

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string value, IErrorFactory errors)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            var err = errors.Validation(
                field: "Email",
                rule: "Required",
                invalidValue: value);
            return Result<Email>.Failure(err, errors);
        }

        if (EmailRegex().IsMatch(value)) return Result<Email>.Success(new Email(value), errors);
        {
            var err = errors.Validation(
                field: "Email",
                rule: "InvalidFormat",
                invalidValue: value);
            return Result<Email>.Failure(err, errors);
        }

    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }

    public override string ToString() => Value;

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();
}