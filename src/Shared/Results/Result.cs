using Shared.Primitives;
using Shared.Results;

public readonly struct Result : IResult
{
    private readonly Result<Unit> _internal;

    public bool IsSuccess => _internal.IsSuccess;
    public bool IsFailure => _internal.IsFailure;
    public Unit Value => _internal.Value;
    public IReadOnlyCollection<Error> Errors => _internal.Errors;
    public Error FirstError => _internal.FirstError;

    private Result(Result<Unit> internalResult) => _internal = internalResult;

    public static Result Success() => new(Result<Unit>.Success(Unit.Value));
    public static Result Failure(Error error) => new(Result<Unit>.Failure(error));
    public static Result Failure(IEnumerable<Error> errors) => new(Result<Unit>.Failure(errors));

    public IResult<TResult> Map<TResult>(Func<Unit, TResult> mapper) => _internal.Map(mapper);
    public IResult<Unit> Tap(Action<Unit> action) => _internal.Tap(action);
    public IResult<Unit> OnFailure(Action<IReadOnlyCollection<Error>> handler) => _internal.OnFailure(handler);
}