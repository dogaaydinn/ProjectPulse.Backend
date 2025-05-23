using System.Collections.Immutable;

namespace Shared.Results;

public sealed class Result<T> : IResult<T>
{
    private readonly T _value;
    private readonly ImmutableArray<Error> _errors;
    private readonly IErrorFactory _errorFactory;
    

    public bool IsSuccess { get; }
    public IErrorFactory ErrorFactory => _errorFactory;
    public bool IsFailure => !IsSuccess;
    public T Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("Cannot access Value of a failed result. Check IsSuccess first.");
    public IReadOnlyCollection<Error> Errors => _errors;
    public Error FirstError => _errors.Length > 0 ? _errors[0] : throw new InvalidOperationException("No errors available.");

    private Result(T value, bool isSuccess, ImmutableArray<Error> errors, IErrorFactory errorFactory)
    {
        _value = value;
        IsSuccess = isSuccess;
        _errors = errors;
        _errorFactory = errorFactory ?? throw new ArgumentNullException(nameof(errorFactory));
    }

    public static Result<T> Success(T value, IErrorFactory errorFactory)
        => new(value, true, ImmutableArray<Error>.Empty, errorFactory);

    public static Result<T> Failure(Error error, IErrorFactory errorFactory)
    {
        ArgumentNullException.ThrowIfNull(error);
        return new Result<T>(default!, false, ImmutableArray.Create(error), errorFactory);
    }

    public static Result<T> Failure(IEnumerable<Error> errors, IErrorFactory errorFactory)
    {
        var arr = errors?.Where(e => true).ToImmutableArray()
                  ?? throw new ArgumentNullException(nameof(errors));
        if (arr.Length == 0) throw new ArgumentException("At least one error required.", nameof(errors));
        return new(default!, false, arr, errorFactory);
    }

    public IResult<TResult> Map<TResult>(Func<T, TResult> mapper)
        => IsSuccess
            ? Result<TResult>.Success(mapper(_value), _errorFactory)
            : Result<TResult>.Failure(_errors, _errorFactory);

    public Task<IResult<TResult>> MapAsync<TResult>(Func<T, Task<TResult>> mapper)
        => IsSuccess
            ? MapAsyncInternal(mapper)
            : Task.FromResult<IResult<TResult>>(Result<TResult>.Failure(_errors, _errorFactory));

    private async Task<IResult<TResult>> MapAsyncInternal<TResult>(Func<T, Task<TResult>> mapper)
        => Result<TResult>.Success(await mapper(_value).ConfigureAwait(false), _errorFactory);

    public IResult<TResult> Bind<TResult>(Func<T, IResult<TResult>> binder)
        => IsSuccess ? binder(_value) : Result<TResult>.Failure(_errors, _errorFactory);

    public Task<IResult<TResult>> BindAsync<TResult>(Func<T, Task<IResult<TResult>>> binder)
        => IsSuccess ? binder(_value) : Task.FromResult<IResult<TResult>>(Result<TResult>.Failure(_errors, _errorFactory));

    public IResult<T> Tap(Action<T> action)
    {
        if (IsSuccess) action(_value);
        return this;
    }

    public Task<IResult<T>> TapAsync(Func<T, Task> action)
    {
        return IsSuccess
            ? TapAsyncInternal(action)
            : Task.FromResult<IResult<T>>(this);
    }

    private async Task<IResult<T>> TapAsyncInternal(Func<T, Task> action)
    {
        await action(_value).ConfigureAwait(false);
        return this;
    }

    public IResult<T> OnFailure(Action<IReadOnlyCollection<Error>> handler)
    {
        if (IsFailure) handler(_errors);
        return this;
    }

    public IResult<T> Ensure(Func<T, bool> predicate, Error error)
        => IsSuccess && !predicate(_value)
            ? Failure(error, _errorFactory)
            : this;

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<IReadOnlyCollection<Error>, TResult> onFailure)
        => IsSuccess ? onSuccess(_value) : onFailure(_errors);

    public Task<TResult> MatchAsync<TResult>(Func<T, Task<TResult>> onSuccess, Func<IReadOnlyCollection<Error>, Task<TResult>> onFailure)
        => IsSuccess ? onSuccess(_value) : onFailure(_errors);

    public void Deconstruct(out bool isSuccess, out T value, out IReadOnlyCollection<Error> errors)
    {
        isSuccess = IsSuccess;
        value = IsSuccess ? _value : default!;
        errors = _errors;
    }
}