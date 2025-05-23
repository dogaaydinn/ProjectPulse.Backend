using Shared.Base;

namespace Shared.Abstractions.Persistence;

public interface IUnitOfWork : IDisposable
{
    ICommandRepository<T> CommandRepository<T>() where T : class, IAggregateRoot<Guid>;
    IQueryRepository<T>   QueryRepository<T>()   where T : class, IAggregateRoot<Guid>;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task DispatchDomainEventsAsync(CancellationToken cancellationToken = default);

    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}