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
    
    public static void IfEndBeforeStart(this ValidationResult result, DateTime start, DateTime? end, Func<Error> errorFactory)
    {
        if (end.HasValue && end.Value < start)
            result.Add(errorFactory());
    }

}
/*
// Shared/Validation/ValidationResultExtensions.cs
   using Shared.Exceptions;
   using Shared.Results;
   
   namespace Shared.Validation;
   
   public static class ValidationResultExtensions
   {
       public static Result<T> ToResult<T>(this IEnumerable<Error> errors)
       {
           var errorList = errors?.Where(e => e is not null).ToList() ?? new();
   
           return errorList.Count == 0
               ? Result<T>.Failure(ErrorFactory.Unexpected("Validation result is empty but marked as invalid."))
               : Result<T>.Failure(errorList);
       }
   
       public static Result ToResult(this IEnumerable<Error> errors)
       {
           var errorList = errors?.Where(e => e is not null).ToList() ?? new();
   
           return errorList.Count == 0
               ? Result.Failure(ErrorFactory.Unexpected("Validation result is empty but marked as invalid."))
               : Result.Failure(errorList);
       }
   
       public static void ThrowIfInvalid(this IEnumerable<Error> errors)
       {
           var errorList = errors?.Where(e => e is not null).ToList() ?? new();
           if (errorList.Count > 0)
               throw new ValidationException(errorList);
       }
   
       // Domain-specific extension
       public static IEnumerable<Error> IfEndBeforeStart(this DateTime start, DateTime? end, Func<Error> errorFactory)
       {
           if (end.HasValue && end.Value < start)
               yield return errorFactory();
       }
   }
   */