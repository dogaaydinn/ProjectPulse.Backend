using Shared.Results;
using Shared.ValueObjects;

namespace Application.Common.Validation;

public static class ValidationResultExtensions
{
    public static Result ToResult(this ValidationResult result)
        => result.IsValid ? Result.Success() : Result.Failure(result.ToErrorList());

    public static List<Error> ToErrorList(this ValidationResult result)
        => result.Errors.Select(e => Error.Validation($"{e.PropertyName}: {e.ErrorMessage}")).ToList();
    
    public static void Add(this ValidationResult result, Error error)
    {
        result.Errors.Add(new ValidationError("Validation", error.Message));
    }

    public static void IfEmptyGuid(this ValidationResult result, Guid value, Func<Error> errorFactory)
    {
        if (value == Guid.Empty)
            result.Add(errorFactory());
    }

    public static void IfEmptyLocalized(this ValidationResult result, LocalizedString? value, Func<Error> errorFactory)
    {
        if (value is null || value.IsEmpty())
            result.Add(errorFactory());
    }

    public static void IfTrue(this ValidationResult result, bool condition, Func<Error> errorFactory)
    {
        if (condition)
            result.Add(errorFactory());
    }

    public static void IfEmptyDateRange(this ValidationResult result, DateRange? value, Func<Error> errorFactory)
    {
        if (value is null || value.IsEmpty())
            result.Add(errorFactory());
    }
    public static void IfEndBeforeStart(this ValidationResult result, DateRange range, Func<Error> errorFactory)
    {
        if (range.End.HasValue && range.End.Value < range.Start)
            result.Add(errorFactory());
    }

}