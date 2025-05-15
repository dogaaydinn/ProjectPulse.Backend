using Application.Common.Validation;
using Shared.Results;

namespace Application.Common.Handlers;

public abstract class BaseCommandHandler<TRequest, TResponse>(IValidator<TRequest> validator)
{
    // ✅ Main execution entry for commands with validation and result<T>
    protected async Task<Result<TResponse>> ValidateAndExecuteAsync(
        TRequest request,
        Func<Task<Result<TResponse>>> execute)
    {
        var validation = validator.Validate(request);
        if (!validation.IsValid)
            return Result<TResponse>.Failure(validation.ToErrorList());

        return await TryExecuteAsync(execute);
    }
    

    // ✅ Error-safe execution for Result<TResponse>
    private async Task<Result<TResponse>> TryExecuteAsync(Func<Task<Result<TResponse>>> action)
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            return Result<TResponse>.Failure(ErrorFactory.Unexpected($"Execution failed: {ex.Message}"));
        }
    }

    // ✅ Error-safe execution for Result (non-generic)
    protected async Task<Result> TryExecuteAsync(Func<Task<Result>> action)
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            return Result.Failure(ErrorFactory.Unexpected($"Execution failed: {ex.Message}"));
        }
    }
}