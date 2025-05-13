using Shared.Results;
using Shared.ValueObjects;

namespace Application.Common.Validation;

public static class ValidationResultExtensions
{
    public static List<Error> ToErrorList(this ValidationResult validationResult)
    {
        return validationResult.Errors
            .Select(e => Error.Validation($"{e.PropertyName}: {e.ErrorMessage}"))
            .ToList();
    }
    public static void IfEmptyGuid(this ValidationResult result, Guid value, string propertyName, string errorMessage)
    {
        if (value == Guid.Empty)
            result.AddError(propertyName, errorMessage);
    }
    public static Result ToResult(this ValidationResult validationResult)
    {
        return validationResult.IsValid
            ? Result.Success()
            : Result.Failure(validationResult.ToErrorList());
    }
    public static ValidationResult IfEmptyLocalized(
        this ValidationResult result,
        LocalizedString? value,
        string propertyName,
        string message)
    {
        if (value is null || value.IsEmpty())
            result.AddError(propertyName, message);
        return result;
    }
    
    public static ValidationResult IfNull(
        this ValidationResult result,
        object? value,
        string propertyName,
        string message)
    {
        if (value is null)
            result.AddError(propertyName, message);
        return result;
    }
    
    public static ValidationResult IfTrue(
        this ValidationResult result,
        bool condition,
        string propertyName,
        string message)
    {
        if (condition)
            result.AddError(propertyName, message);
        return result;
    }

}