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

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<Email>.Failure(Error.Validation("Email cannot be empty"));

        return !EmailRegex().IsMatch(value) ? Result<Email>.Failure(Error.Validation("Invalid email format")) : Result<Email>.Success(new Email(value));
    }


    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }

    public override string ToString() => Value;

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();
}