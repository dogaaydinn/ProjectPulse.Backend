using Shared.Events;

namespace Shared.Base;

public abstract class AggregateRoot<TId> : AuditableEntity<TId>, IAggregateRoot<TId> where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public long DomainVersion { get; private set; }

    protected AggregateRoot(TId id) : base(id) { }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        _domainEvents.Add(domainEvent);
        DomainVersion++;
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}