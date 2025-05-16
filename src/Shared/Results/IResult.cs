using Shared.Primitives;

namespace Shared.Results;

public interface IResult<out T>
{
    bool IsSuccess { get; }
    bool IsFailure => !IsSuccess;
    T? Value { get; }
    IReadOnlyCollection<Error> Errors { get; }
    Error FirstError { get; }
    
    IResult<TResult> Map<TResult>(Func<T, TResult> mapper);
    IResult<T> Tap(Action<T> action);
    IResult<T> OnFailure(Action<IReadOnlyCollection<Error>> handler);
}

public interface IResult : IResult<Unit>{}