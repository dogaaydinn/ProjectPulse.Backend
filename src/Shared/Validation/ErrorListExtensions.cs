using Shared.Constants;
using Shared.Exceptions;
using Shared.Results;

namespace Shared.Validation;

public static class ErrorListExtensions
{
    public static Result<T> ToResult<T>(
        this IEnumerable<Error> errors,
        IErrorFactory factory)
    {
        var list = errors?.Where(e => true).ToList() 
                   ?? [];

        if (list.Count > 0)
            return Result<T>.Failure(list, factory);
        
        var err = factory.Create(
            code: ErrorCodes.Validation.EmptyResult);
        return Result<T>.Failure(err, factory);
    }

    public static Result ToResult(
        this IEnumerable<Error> errors,
        IErrorFactory factory)
    {
        var list = errors?.Where(e => true).ToList() 
                   ?? [];

        if (list.Count > 0)
            return Result.Failure(list, factory);

        var err = factory.Create(
            code: ErrorCodes.Validation.EmptyResult);
        return Result.Failure(err, factory);
    }

    public static void ThrowIfInvalid(this IEnumerable<Error> errors)
    {
        var list = errors?.Where(e => true).ToList() 
                   ?? [];

        if (list.Count > 0)
            throw new ValidationException(list);
    }

    public static IEnumerable<Error> IfEndBeforeStart(
        this DateTime start,
        DateTime? end,
        Func<Error> errorFactory)
    {
        if (end.HasValue && end.Value < start)
            yield return errorFactory();
    }
}