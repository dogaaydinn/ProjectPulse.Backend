using Shared.Primitives;

namespace Shared.Results;

public sealed class Result : IResult
{
    private readonly Result<Unit> _inner;

    public IErrorFactory ErrorFactory => _inner.ErrorFactory;
    private Result(Result<Unit> inner) => _inner = inner;

    public bool IsSuccess => _inner.IsSuccess;
    public bool IsFailure => _inner.IsFailure;

    public Unit Value => _inner.Value;
    public IReadOnlyCollection<Error> Errors => _inner.Errors;
    public Error FirstError => _inner.FirstError;

    public static Result Success(IErrorFactory factory)
        => new(Result<Unit>.Success(Unit.Value, factory));

    public static Result Failure(Error error, IErrorFactory factory)
        => new(Result<Unit>.Failure(error, factory));

    public static Result Failure(IEnumerable<Error> errors, IErrorFactory factory)
        => new(Result<Unit>.Failure(errors, factory));

    public static Result Try(Action action, IErrorFactory factory, Func<Exception, Error>? converter = null)
    {
        try
        {
            action();
            return Success(factory);
        }
        catch (Exception ex)
        {
            var err = converter?.Invoke(ex) ?? factory.Unexpected(ex.Message);
            return Failure(err, factory);
        }
    }

    public static async Task<Result> TryAsync(Func<Task> action, IErrorFactory factory, Func<Exception, Error>? converter = null)
    {
        try
        {
            await action().ConfigureAwait(false);
            return Success(factory);
        }
        catch (Exception ex)
        {
            var err = converter?.Invoke(ex) ?? factory.Unexpected(ex.Message);
            return Failure(err, factory);
        }
    }

    public IResult<TResult> Map<TResult>(Func<Unit, TResult> mapper)
        => _inner.Map(mapper);

    public Task<IResult<TResult>> MapAsync<TResult>(Func<Unit, Task<TResult>> mapper)
        => _inner.MapAsync(mapper);

    public IResult<TResult> Bind<TResult>(Func<Unit, IResult<TResult>> binder)
        => _inner.Bind(binder);

    public Task<IResult<TResult>> BindAsync<TResult>(Func<Unit, Task<IResult<TResult>>> binder)
        => _inner.BindAsync(binder);

    public IResult<Unit> Tap(Action<Unit> action)
        => _inner.Tap(action);

    public Result Tap(Action action)
    {
        if (IsSuccess) action();
        return this;
    }

    public Task<IResult<Unit>> TapAsync(Func<Unit, Task> action)
        => _inner.TapAsync(action);

    public async Task<Result> TapAsync(Func<Task> action)
    {
        if (IsSuccess) await action().ConfigureAwait(false);
        return this;
    }

    public IResult<Unit> OnFailure(Action<IReadOnlyCollection<Error>> handler)
        => _inner.OnFailure(handler);

    public IResult<Unit> Ensure(Func<Unit, bool> predicate, Error error)
        => _inner.Ensure(predicate, error);

    public Result Ensure(Func<bool> predicate, Error error)
    {
        if (IsFailure) return this;
        return predicate() ? this : Failure(error, _inner.ErrorFactory);
    }

    public TResult Match<TResult>(Func<Unit, TResult> onSuccess, Func<IReadOnlyCollection<Error>, TResult> onFailure)
        => _inner.Match(onSuccess, onFailure);

    public Task<TResult> MatchAsync<TResult>(Func<Unit, Task<TResult>> onSuccess, Func<IReadOnlyCollection<Error>, Task<TResult>> onFailure)
        => _inner.MatchAsync(onSuccess, onFailure);

    public void Deconstruct(out bool isSuccess, out Unit value, out IReadOnlyCollection<Error> errors)
    {
        isSuccess = IsSuccess;
        value = IsSuccess ? Unit.Value : default;
        errors = Errors;
    }
}