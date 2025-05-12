namespace Shared.Results;

public class Result : IResult
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public List<Error> Errors { get; }
    public Error? Error => Errors.FirstOrDefault();

    protected Result(bool isSuccess, List<Error>? errors = null)
    {
        IsSuccess = isSuccess;
        Errors = errors ?? new();
    }

    public static Result Success() => new(true);
    public static Result Failure(Error error) => new(false, new List<Error> { error });
    public static Result Failure(List<Error> errors) => new(false, errors);
}