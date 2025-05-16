using Shared.Exceptions;
using Shared.Results;
using Shared.Time;

namespace Shared.Validation;

public static class Guard
{
    public static void AgainstNull<T>(T? value, Func<Error> errorFactory)
    {
        if (value is null)
            throw new AppException(errorFactory());
    }

    public static void AgainstNullOrWhiteSpace(string? value, Func<Error> errorFactory)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new AppException(errorFactory());
    }

    public static void AgainstDefaultGuid(Guid id, Func<Error> errorFactory)
    {
        if (id == Guid.Empty)
            throw new AppException(errorFactory());
    }

    public static void InRangeOf<T>(T value, T min, T max, Func<Error> errorFactory) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            throw new AppException(errorFactory());
    }

    public static void AgainstPastDate(DateTime value, IClock clock, Func<Error> errorFactory)
    {
        if (value < clock.UtcNow)
            throw new AppException(errorFactory());
    }

    public static void AgainstFutureDate(DateTime value, IClock clock, Func<Error> errorFactory)
    {
        if (value > clock.UtcNow)
            throw new AppException(errorFactory());
    }

    public static void Unless(bool condition, Func<Error> errorFactory)
    {
        if (condition)
            throw new AppException(errorFactory());
    }

    public static void All(params (bool Condition, Func<Error> ErrorFactory)[] validations)
    {
        foreach (var (condition, error) in validations)
        {
            if (!condition)
                throw new AppException(error());
        }
    }
}