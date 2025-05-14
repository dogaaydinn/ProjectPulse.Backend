using Shared.Results;
using Shared.ValueObjects;

namespace Application.Common.Validation;

public static class ValidationResultExtensions
{
    // ✅ Fluent string-based overloads
    public static ValidationResult IfEmptyGuid(this ValidationResult result, Guid value, string propertyName, string message)
    {
        if (value == Guid.Empty)
            result.AddError(propertyName, message);
        return result;
    }

    public static ValidationResult IfEmptyLocalized(this ValidationResult result, LocalizedString? value, string propertyName, string message)
    {
        if (value is null || value.IsEmpty())
            result.AddError(propertyName, message);
        return result;
    }

    public static ValidationResult IfTrue(this ValidationResult result, bool condition, string propertyName, string message)
    {
        if (condition)
            result.AddError(propertyName, message);
        return result;
    }

    public static ValidationResult IfFalse(this ValidationResult result, bool condition, string propertyName, string message)
    {
        if (!condition)
            result.AddError(propertyName, message);
        return result;
    }

    public static ValidationResult IfNull<T>(this ValidationResult result, T? value, string propertyName, string message)
    {
        if (value is null)
            result.AddError(propertyName, message);
        return result;
    }

    public static ValidationResult IfEmptyString(this ValidationResult result, string? value, string propertyName, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
            result.AddError(propertyName, message);
        return result;
    }

    public static ValidationResult IfOutOfRange<T>(this ValidationResult result, T value, T min, T max, string propertyName, string message)
        where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            result.AddError(propertyName, message);
        return result;
    }

    public static ValidationResult IfEmptyDateRange(this ValidationResult result, DateRange? value, string propertyName, string message)
    {
        if (value is null || value.IsEmpty())
            result.AddError(propertyName, message);
        return result;
    }

    // ✅ Func<Error> - Error Factory overloads (Enterprise-style)
    public static void Add(this ValidationResult result, Error error)
    {
        var validationError = ValidationError.Create(error.Code, error.Message);
        result.Errors.Add(validationError);
    }

    public static ValidationResult IfEmptyGuid(this ValidationResult result, Guid value, Func<Error> error)
    {
        if (value == Guid.Empty)
            result.Add(error());
        return result;
    }

    public static ValidationResult IfEmptyLocalized(this ValidationResult result, LocalizedString? value, Func<Error> error)
    {
        if (value is null || value.IsEmpty())
            result.Add(error());
        return result;
    }

    public static ValidationResult IfTrue(this ValidationResult result, bool condition, Func<Error> error)
    {
        if (condition)
            result.Add(error());
        return result;
    }

    public static ValidationResult IfFalse(this ValidationResult result, bool condition, Func<Error> error)
    {
        if (!condition)
            result.Add(error());
        return result;
    }

    public static ValidationResult IfNull<T>(this ValidationResult result, T? value, Func<Error> error)
    {
        if (value is null)
            result.Add(error());
        return result;
    }

    public static ValidationResult IfEmptyDateRange(this ValidationResult result, DateRange? value, Func<Error> error)
    {
        if (value is null || value.IsEmpty())
            result.Add(error());
        return result;
    }

    public static ValidationResult IfEmptyString(this ValidationResult result, string? value, Func<Error> error)
    {
        if (string.IsNullOrWhiteSpace(value))
            result.Add(error());
        return result;
    }

    public static ValidationResult IfOutOfRange<T>(this ValidationResult result, T value, T min, T max, Func<Error> error)
        where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            result.Add(error());
        return result;
    }

    // Convert to Shared.Result
    public static Result ToResult(this ValidationResult result)
    {
        return result.IsValid ? Result.Success() : Result.Failure(result.ToErrorList());
    }

    public static List<Error> ToErrorList(this ValidationResult result)
    {
        return result.Errors
            .Select(e => Error.Validation($"{e.PropertyName}: {e.ErrorMessage}"))
            .ToList();
    }
}
