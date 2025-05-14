using Shared.Exceptions;
using Shared.Results;
using Shared.Time;
using Shared.ValueObjects;

namespace Shared.Validation;

public static class Guard
{
    public static void AgainstNull(object? value, Func<Error> errorFactory)
    {
        if (value is null)
            throw new AppException(errorFactory());
    }

    public static void AgainstNullOrEmpty(string? input, Func<Error> errorFactory)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new AppException(errorFactory());
    }

    public static void AgainstEmpty(string value, Func<Error> errorFactory)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new AppException(errorFactory());
    }

    public static void AgainstEmptyLocalized(LocalizedString? value, Func<Error> errorFactory)
    {
        if (value is null || value.IsEmpty())
            throw new AppException(errorFactory());
    }

    public static void AgainstInvalidCondition(bool condition, Func<Error> errorFactory)
    {
        if (condition)
            throw new AppException(errorFactory());
    }

    public static void AgainstDefaultGuid(Guid id, Func<Error> errorFactory)
    {
        if (id == Guid.Empty)
            throw new AppException(errorFactory());
    }

    public static void AgainstEmptyDateRange(DateRange? value, Func<Error> errorFactory)
    {
        if (value is null || value.IsEmpty())
            throw new AppException(errorFactory());
    }

    public static void AgainstPastDate(DateTime date, Func<Error> errorFactory)
    {
        if (date < DateTime.UtcNow)
            throw new AppException(errorFactory());
    }

    public static void AgainstFutureDate(DateTime date, Func<Error> errorFactory)
    {
        if (date > DateTime.UtcNow)
            throw new AppException(errorFactory());
    }

    public static void AgainstOutOfRange<T>(T value, T min, T max, Func<Error> errorFactory)
        where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            throw new AppException(errorFactory());
    }

    public static void AgainstNullOrWhiteSpace(string? value, Func<Error> errorFactory)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new AppException(errorFactory());
    }

    public static void AgainstEmptyGuid(Guid id, Func<Error> errorFactory)
    {
        if (id == Guid.Empty)
            throw new AppException(errorFactory());
    }
    public static void AgainstPastDate(DateTime date, IDateTimeProvider clock, Error error)
    {
        if (date < clock.UtcNow)
            throw new AppException(error.Message, error.Code);
    }

}