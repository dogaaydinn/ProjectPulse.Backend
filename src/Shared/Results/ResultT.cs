namespace Shared.Results;

public class Result<T> : Result
{
    public T? Value { get; }

    protected Result(T? value, bool isSuccess, List<Error>? errors = null)
        : base(isSuccess, errors)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(value, true);
    public static new Result<T> Failure(Error error) => new(default, false, new List<Error> { error });
    public static new Result<T> Failure(List<Error> errors) => new(default, false, errors);
}