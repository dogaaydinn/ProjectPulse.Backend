using Shared.Primitives;

namespace Shared.Results;

    public interface IResult<T>
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }
        T Value { get; }
        IReadOnlyCollection<Error> Errors { get; }
        IErrorFactory ErrorFactory { get; }
        Error FirstError { get; }
        Task<IResult<TResult>> MapAsync<TResult>(Func<T, Task<TResult>> mapper);
        IResult<TResult> Bind<TResult>(Func<T, IResult<TResult>> binder);
        Task<IResult<TResult>> BindAsync<TResult>(Func<T, Task<IResult<TResult>>> binder);
        IResult<T> Tap(Action<T> action);
        Task<IResult<T>> TapAsync(Func<T, Task> action);
        IResult<T> OnFailure(Action<IReadOnlyCollection<Error>> handler);
        IResult<T> Ensure(Func<T, bool> predicate, Error error);
        TResult Match<TResult>(Func<T, TResult> onSuccess, Func<IReadOnlyCollection<Error>, TResult> onFailure);
        Task<TResult> MatchAsync<TResult>(Func<T, Task<TResult>> onSuccess, Func<IReadOnlyCollection<Error>, Task<TResult>> onFailure);
        void Deconstruct(out bool isSuccess, out T value, out IReadOnlyCollection<Error> errors);
    }

    public interface IResult : IResult<Unit> { }