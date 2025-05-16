namespace Shared.Events;

public interface IDomainEvent
{
    Guid EventId { get; }
    DateTime OccurredOn { get; }
    string EventType { get; }
    IReadOnlyDictionary<string, object>? Metadata { get; }
}