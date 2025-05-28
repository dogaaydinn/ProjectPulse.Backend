using Shared.Base;

namespace Domain.Core.Persistence;

public interface IRepositoryFactory
{
    IRepository<T> GetRepository<T>()
        where T : class, IAggregateRoot<Guid>;
}