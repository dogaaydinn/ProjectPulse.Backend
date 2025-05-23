using Shared.Base;

namespace Shared.Abstractions.Persistence;

public interface ICommandRepository<T> where T : class, IAggregateRoot<Guid>
{
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Remove(T entity);
}