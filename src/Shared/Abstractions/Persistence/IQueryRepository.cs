using Shared.Base;

namespace Shared.Abstractions.Persistence;

public interface IQueryRepository<T> where T : class, IAggregateRoot<Guid>
{
    Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
    IQueryable<T> Query();
}