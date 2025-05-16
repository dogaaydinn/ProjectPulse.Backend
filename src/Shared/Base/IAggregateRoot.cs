using Shared.Events;

namespace Shared.Base;

public interface IAggregateRoot<out TId> : IEntity<TId>
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
    long Version { get; }
}