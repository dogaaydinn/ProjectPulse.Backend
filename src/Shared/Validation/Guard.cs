using Shared.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Shared.Validation;

public static class Guard
{
    public static void AgainstNull<T>([NotNull] T? input, string errorCode, string message)
    {
        if (input is null)
            throw new AppException(errorCode, message);
    }

    public static void AgainstNullOrEmpty(string? input, string errorCode, string message)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new AppException(errorCode, message);
    }

    public static void AgainstInvalidCondition(bool condition, string errorCode, string message)
    {
        if (condition)
            throw new AppException(errorCode, message);
    }

    public static void AgainstOutOfRange<T>(T value, T min, T max, string errorCode, string message)
        where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            throw new AppException(errorCode, message);
    }

    public static void AgainstInvalidEmail(string email, string errorCode, string message)
    {
        if (!email.Contains('@') || !email.Contains('.'))
            throw new AppException(errorCode, message);
    }

    public static void AgainstDefaultGuid(Guid id, string errorCode, string message)
    {
        if (id == Guid.Empty)
            throw new AppException(errorCode, message);
    }

    public static void AgainstEmpty(string value, string errorCode, string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new AppException(errorCode, errorMessage);
    }

    public static void AgainstEmptyLocalized(LocalizedString? value, string errorCode, string errorMessage)
    {
        if (value is null || value.IsEmpty())
            throw new AppException(errorCode, errorMessage);
    }
}