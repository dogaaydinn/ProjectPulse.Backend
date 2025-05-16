using Shared.Events;

namespace Shared.Base;

public abstract class AggregateRoot<TId>(TId id) : AuditableEntity<TId>(id), IAggregateRoot<TId>
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public new long Version { get; private set; }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent ?? throw new ArgumentNullException(nameof(domainEvent)));
        Version++;
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected virtual void Apply(IDomainEvent @event)
        => throw new NotImplementedException($"Apply not implemented for {@event.GetType().Name}");
}

public abstract class AggregateRoot() : AggregateRoot<Guid>(Guid.NewGuid()), IEntity<Guid>
{
    Guid IEntity<Guid>.Id => Id;
    int IEntity<Guid>.Version => checked((int)Version);
}