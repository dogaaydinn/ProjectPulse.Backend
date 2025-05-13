using Shared.Events;

namespace Domain.Events;

public sealed class UserCreatedDomainEvent : IDomainEvent
{
    public Guid UserId { get; }

    public UserCreatedDomainEvent(Guid userId, DateTime occurredOn)
    {
        UserId = userId;
        OccurredOn = occurredOn;
    }

    public DateTime OccurredOn { get; }
}