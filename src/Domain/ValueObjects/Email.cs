using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public sealed class Email
{
    private string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.");

        var isValid = Regex.IsMatch(email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.IgnoreCase);

        if (!isValid)
            throw new ArgumentException("Email format is invalid.");

        return new Email(email.Trim().ToLowerInvariant());
    }

    public override string ToString() => Value;

    public override bool Equals(object? obj)
    {
        if (obj is not Email other) return false;
        return Value == other.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();
}