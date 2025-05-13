using Shared.Constants;
using Shared.Results;

namespace Application.Common.Handlers;

public abstract class BaseQueryHandler<TQuery, TResult>
{
    protected async Task<Result<TResult>> TryExecuteAsync(
        Func<Task<Result<TResult>>> action,
        string failureMessage = ErrorMessages.QueryExecutionFailed)
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            return Result<TResult>.Failure(Error.Unexpected(failureMessage + " Reason: " + ex.Message));
        }
    }
}