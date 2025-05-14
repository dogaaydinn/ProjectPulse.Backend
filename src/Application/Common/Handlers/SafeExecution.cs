using Shared.Constants;
using Shared.Results;

namespace Application.Common.Handlers;

public static class SafeExecution
{
    public static async Task<Result<T>> TryExecuteAsync<T>(
        Func<Task<Result<T>>> action,
        string failureMessage = ErrorMessages.QueryExecutionFailed)
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            return Result<T>.Failure(Error.Unexpected($"{failureMessage} Reason: {ex.Message}"));
        }
    }
}