using Application.Common.Validation;
using Shared.Results;

namespace Application.Common.Handlers;

public abstract class BaseCommandHandler<TRequest, TResponse>
{
    private readonly IValidator<TRequest> _validator;

    protected BaseCommandHandler(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    protected async Task<Result<TResponse>> ValidateAndExecuteAsync(
        TRequest request,
        Func<Task<Result<TResponse>>> execute)
    {
        var validation = _validator.Validate(request);
        if (!validation.IsValid)
            return Result<TResponse>.Failure(validation.ToErrorList());

        return await execute();
    }

    protected async Task<Result> ValidateAndExecuteAsync(
        TRequest request,
        Func<Task<Result>> execute)
    {
        var validation = _validator.Validate(request);
        if (!validation.IsValid)
            return Result.Failure(validation.ToErrorList());

        return await execute();
    }
}