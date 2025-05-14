using Shared.Results;
using Shared.ValueObjects;

namespace Application.Common.Validation;

public static class ValidationResultExtensions
{
    public static Result ToResult(this ValidationResult result)
    {
        return result.IsValid
            ? Result.Success()
            : Result.Failure(result.ToErrorList());
    }

    public static List<Error> ToErrorList(this ValidationResult result)
    {
        return result.Errors
            .Select(e => Error.Validation($"{e.PropertyName}: {e.ErrorMessage}"))
            .ToList();
    }

    public static void IfEmptyGuid(this ValidationResult result, Guid value, string propertyName, string message)
    {
        if (value == Guid.Empty)
            result.AddError(propertyName, message);
    }

    public static void IfNull(this ValidationResult result, object? value, string propertyName, string message)
    {
        if (value is null)
            result.AddError(propertyName, message);
    }

    public static void IfTrue(this ValidationResult result, bool condition, string propertyName, string message)
    {
        if (condition)
            result.AddError(propertyName, message);
    }

    public static void IfFalse(this ValidationResult result, bool condition, string propertyName, string message)
    {
        if (!condition)
            result.AddError(propertyName, message);
    }

    public static void IfEmptyString(this ValidationResult result, string? value, string propertyName, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
            result.AddError(propertyName, message);
    }

    public static void IfOutOfRange<T>(this ValidationResult result, T value, T min, T max, string propertyName, string message)
        where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            result.AddError(propertyName, message);
    }

    public static void IfEmptyLocalized(this ValidationResult result, LocalizedString? value, string propertyName, string message)
    {
        if (value is null || value.IsEmpty())
            result.AddError(propertyName, message);
    }

    public static void IfEmptyDateRange(this ValidationResult result, DateRange? value, string propertyName, string message)
    {
        if (value is null || value.IsEmpty())
            result.AddError(propertyName, message);
    }
}
