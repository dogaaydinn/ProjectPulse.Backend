using Shared.Exceptions;
using Shared.Results;
using Shared.Time;
using Shared.ValueObjects;

namespace Shared.Validation;

public static partial class Guard
{
    public static Guid EnsureNotEmptyGuid(Guid id, Func<Error> errorFactory)
    {
        if (id == Guid.Empty)
            throw new AppException(errorFactory());
        return id;
    }

    public static string EnsureNotNullOrWhiteSpace(string? value, Func<Error> errorFactory)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new AppException(errorFactory());
        return value;
    }

    public static T EnsureNotNull<T>(T? value, Func<Error> errorFactory)
    {
        if (value is null)
            throw new AppException(errorFactory());
        return value;
    }

    public static LocalizedString EnsureNotEmptyLocalized(LocalizedString? value, Func<Error> errorFactory)
    {
        if (value is null || value.IsEmpty())
            throw new AppException(errorFactory());
        return value;
    }

    public static DateRange EnsureNotEmptyDateRange(DateRange? value, Func<Error> errorFactory)
    {
        if (value is null || value.IsEmpty())
            throw new AppException(errorFactory());
        return value;
    }

    public static T EnsureInRange<T>(T value, T min, T max, Func<Error> errorFactory)
        where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            throw new AppException(errorFactory());
        return value;
    }

    public static DateTime EnsureNotPast(DateTime value, IDateTimeProvider clock, Func<Error> errorFactory)
    {
        if (value < clock.UtcNow)
            throw new AppException(errorFactory());
        return value;
    }

    public static DateTime EnsureNotFuture(DateTime value, IDateTimeProvider clock, Func<Error> errorFactory)
    {
        if (value > clock.UtcNow)
            throw new AppException(errorFactory());
        return value;
    }
}