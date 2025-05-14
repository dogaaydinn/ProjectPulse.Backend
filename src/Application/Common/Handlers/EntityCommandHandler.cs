using Application.Common.Validation;
using Domain.Core.Persistence;
using Shared.Results;

namespace Application.Common.Handlers;

public abstract class EntityCommandHandler<TRequest, TEntity>
    : BaseCommandHandler<TRequest, Guid>
    where TEntity : class
{
    protected readonly IRepository<TEntity> Repository;
    protected readonly IUnitOfWork UnitOfWork;

    protected EntityCommandHandler(
        IRepository<TEntity> repository,
        IUnitOfWork unitOfWork,
        IValidator<TRequest> validator)
        : base(validator)
    {
        Repository = repository;
        UnitOfWork = unitOfWork;
    }

    protected async Task<Result<TEntity>> GetEntityAsync(Guid id, string entityName)
    {
        return await SafeExecution.TryExecuteAsync(async () =>
        {
            var entity = await Repository.GetByIdAsync(id);
            return entity is null
                ? Result<TEntity>.Failure(Error.NotFound(entityName, id))
                : Result<TEntity>.Success(entity);
        });
    }


    protected async Task<Result<Guid>> SaveAndReturnIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await SafeExecution.TryExecuteAsync(async () =>
        {
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Guid>.Success(id);
        });
    }
}