using Shared.Exceptions;
using Shared.Results;
using Shared.ValueObjects;

namespace Shared.Validation;

public static class Ensure
{
    public static T Required<T>(T? value, Func<Error> errorFactory)
    {
        if (value is null || value.Equals(default(T)))
            throw new AppException(errorFactory());
        return value;
    }

    public static string NotEmpty(string? value, Func<Error> errorFactory)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new AppException(errorFactory());
        return value.Trim();
    }

    public static Guid NotDefault(Guid value, Func<Error> errorFactory)
    {
        if (value == Guid.Empty)
            throw new AppException(errorFactory());
        return value;
    }

    public static DateRange NotEmptyRange(DateRange? value, Func<Error> errorFactory)
    {
        if (value is null || value.IsEmpty())
            throw new AppException(errorFactory());
        return value;
    }

    public static LocalizedString NotEmptyLocalized(LocalizedString? value, Func<Error> errorFactory)
    {
        if (value is null || value.IsEmpty())
            throw new AppException(errorFactory());
        return value;
    }

    public static T InRange<T>(T value, T min, T max, Func<Error> errorFactory) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            throw new AppException(errorFactory());
        return value;
    }

    public static void That(bool condition, Func<Error> errorFactory)
    {
        if (!condition)
            throw new AppException(errorFactory());
    }
}