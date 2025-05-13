using System.Text.RegularExpressions;
using Shared.Exceptions;

namespace Shared.ValueObjects;

public sealed partial class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new AppException("Validation.Email.Empty", "Email address cannot be empty.");

        if (!MyRegex().IsMatch(value))
            throw new AppException("Validation.Email.Invalid", "Email address is invalid.");

        Value = value;
    }

    public static Email Create(string value) => new(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }

    public override string ToString() => Value;
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex MyRegex();
}