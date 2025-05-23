namespace Shared.Results;

public static class ResultExtensions
{
    
    public static Result<TResult> Bind<T, TResult>(
        this IResult<T> result, 
        Func<T, Result<TResult>> binder)
        => result.IsSuccess 
            ? binder(result.Value) 
            : Result<TResult>.Failure(result.Errors, result.ErrorFactory);
    
    public static Task<Result<TResult>> BindAsync<T, TResult>(
        this IResult<T> result,
        Func<T, Task<Result<TResult>>> asyncBinder)
        => result.IsSuccess 
            ? asyncBinder(result.Value) 
            : Task.FromResult(Result<TResult>.Failure(result.Errors, result.ErrorFactory));
    
    public static Task<Result<TResult>> MapAsync<T, TResult>(
        this IResult<T> result,
        Func<T, Task<TResult>> asyncMapper)
        => result.IsSuccess
            ? asyncMapper(result.Value).ContinueWith(t =>
                Result<TResult>.Success(t.Result, result.ErrorFactory))
            : Task.FromResult(Result<TResult>.Failure(result.Errors, result.ErrorFactory));
    
    public static IResult<T> Ensure<T>(
        this IResult<T> result,
        Func<T, bool> predicate,
        Error error)
        => result.IsFailure
            ? result
            : (predicate(result.Value)
                ? result
                : Result<T>.Failure(error, result.ErrorFactory));
    
    public static Result<IEnumerable<T>> Combine<T>(
        this IEnumerable<IResult<T>> results,
        IErrorFactory errorFactory)
    {
        var values = new List<T>();
        var errors = new List<Error>();

        foreach (var res in results)
        {
            if (res.IsSuccess)
                values.Add(res.Value);
            else
                errors.AddRange(res.Errors);
        }

        return errors.Count > 0
            ? Result<IEnumerable<T>>.Failure(errors, errorFactory)
            : Result<IEnumerable<T>>.Success(values, errorFactory);
    }

    
    public static void Deconstruct<T>(
        this Result<T> result,
        out bool isSuccess,
        out T value,
        out IReadOnlyCollection<Error> errors)
    {
        isSuccess = result.IsSuccess;
        value = result.IsSuccess ? result.Value : default!;
        errors = result.Errors;
    }
}