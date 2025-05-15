using Shared.Events;

namespace Domain.Events;

public sealed class UserCreatedDomainEvent(Guid userId, DateTime occurredOn) : IDomainEvent
{
    public Guid UserId { get; } = userId;

    public DateTime OccurredOn { get; } = occurredOn;
}