namespace Shared.Events;

public abstract record DomainEvent : IDomainEvent
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
    public abstract string EventType { get; }
    public IReadOnlyDictionary<string, object>? Metadata { get; private init; } = new Dictionary<string, object>();

    public DomainEvent WithMetadata(string key, object value)
    {
        var dict = Metadata is null ? new Dictionary<string, object>() : new Dictionary<string, object>(Metadata);
        dict[key] = value;
        return this with { Metadata = dict };
    }
}