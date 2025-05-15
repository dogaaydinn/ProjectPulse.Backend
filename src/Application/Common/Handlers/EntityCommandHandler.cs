using Application.Common.Validation;
using Domain.Core.Persistence;
using Shared.Results;

namespace Application.Common.Handlers;

public abstract class EntityCommandHandler<TRequest, TEntity>(
    IRepository<TEntity> repository,
    IUnitOfWork unitOfWork,
    IValidator<TRequest> validator)
    : BaseCommandHandler<TRequest, Guid>(validator)
    where TEntity : class
{
    protected async Task<Result<TEntity>> GetEntityAsync(Guid id, string entityName)
    {
        return await SafeExecution.TryExecuteAsync(async () =>
        {
            var entity = await repository.GetByIdAsync(id);
            return entity is null
                ? Result<TEntity>.Failure(Error.NotFound(entityName, id))
                : Result<TEntity>.Success(entity);
        });
    }


    protected async Task<Result<Guid>> SaveAndReturnIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await SafeExecution.TryExecuteAsync(async () =>
        {
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Guid>.Success(id);
        });
    }
}