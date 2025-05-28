using Shared.Time;

namespace Infrastructure.Messaging.Contracts;


public sealed class OutboxMessage
{
    public Guid Id { get; init; }
    public string Type { get; init; }
    public string Payload { get; init; }
    public DateTime OccurredOnUtc { get; init; }
    public DateTime? UpdatedAtUtc { get; private set; }

    public bool Processed { get; private set; }
    public DateTime? ProcessedOnUtc { get; private set; }
    public int RetryCount { get; private set; }
    public DateTime? NextRetryAtUtc { get; private set; }
    public string? LastError { get; private set; }
    public MessagePriority Priority { get; private set; }
    public string? LockedByInstance { get; private set; }
    public DateTime? LockedUntilUtc { get; private set; }

    // EF Core veya Dapper için parameterless ctor
    private OutboxMessage() { }

    public OutboxMessage(Guid id, string type, string payload, DateTime occurredOnUtc)
    {
        Id = id;
        Type = type;
        Payload = payload;
        OccurredOnUtc = occurredOnUtc;
        Priority = MessagePriority.Normal;
        UpdatedAtUtc   = occurredOnUtc;
    }

    public void IncrementRetry(IClock clock)
    {
        RetryCount++;
        UpdatedAtUtc = clock.UtcNow;
    }


    public void MarkAsProcessed(IClock clock)
    {
        Processed      = true;
        ProcessedOnUtc = clock.UtcNow;
        UpdatedAtUtc   = clock.UtcNow;
    }

    public void SetNextRetry(DateTime nextRetryAtUtc, IClock clock)
    {
        NextRetryAtUtc = nextRetryAtUtc;
        UpdatedAtUtc = clock.UtcNow;
    }
    public void SetLastError(string error, IClock clock)
    {
        LastError = error;
        UpdatedAtUtc = clock.UtcNow;
    }

    public void SetPriority(MessagePriority priority, IClock clock)
    {
        Priority = priority;
        UpdatedAtUtc = clock.UtcNow;
    }

    public void SetLockInfo(string instance, DateTime untilUtc, IClock clock)
    {
        LockedByInstance = instance;
        LockedUntilUtc = untilUtc;
        UpdatedAtUtc = clock.UtcNow;
    }

    public void ClearLockInfo(IClock clock)
    {
        LockedByInstance = null;
        LockedUntilUtc = null;
        UpdatedAtUtc = clock.UtcNow;
    }
}

