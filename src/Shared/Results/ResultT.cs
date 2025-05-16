namespace Shared.Results;

public readonly struct Result<T> : IResult<T>
{
    private readonly T? _value;
    private readonly List<Error>? _errors;

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value => IsSuccess ? _value : default;
    public IReadOnlyCollection<Error> Errors
    {
        get
        {
            if (_errors is { Count: > 0 })
                return _errors.AsReadOnly();

            return [];
        }
    }

    public Error FirstError => Errors.FirstOrDefault() ?? Error.Unexpected("No error provided");

    private Result(T? value, bool isSuccess, List<Error>? errors = null)
    {
        _value = value;
        IsSuccess = isSuccess;
        _errors = errors;
    }

    public static Result<T> Success(T value) => new(value, true);
    public static Result<T> Failure(Error error) => new(default, false, [error]);
    public static Result<T> Failure(IEnumerable<Error> errors) => new(default, false, errors?.Where(e => e != null).ToList());

    public static Result<T> From(Func<T> func, Func<Exception, Error>? errorHandler = null)
    {
        try
        {
            return Success(func());
        }
        catch (Exception ex)
        {
            var error = errorHandler?.Invoke(ex) ?? Error.Unexpected(ex.Message)
                .WithMetadata("ExceptionType", ex.GetType().Name);
            return Failure(error);
        }
    }

    public IResult<TResult> Map<TResult>(Func<T, TResult> mapper) =>
        IsSuccess
            ? Result<TResult>.Success(mapper(_value!))
            : Result<TResult>.Failure(Errors);

    public IResult<T> Tap(Action<T> action)
    {
        if (IsSuccess) action(_value!);
        return this;
    }

    public IResult<T> OnFailure(Action<IReadOnlyCollection<Error>> handler)
    {
        if (IsFailure) handler(Errors);
        return this;
    }

    public void Deconstruct(out bool isSuccess, out T? value, out IReadOnlyCollection<Error> errors)
    {
        isSuccess = IsSuccess;
        value = _value;
        errors = Errors;
    }
}