using Shared.Time;

namespace Shared.Messaging.Contracts;

public enum MessagePriority
{
    Low = 0,
    Normal = 1,
    High = 2,
    Critical = 3
}

public sealed record OutboxMessage
{
    public Guid      Id               { get; init; }
    public string    Type             { get; init; }
    public string    Payload          { get; init; }
    public DateTime  OccurredOnUtc    { get; init; }
    public bool      Processed        { get; init; }
    public DateTime? ProcessedOnUtc   { get; init; }
    public int       RetryCount       { get; init; }
    public DateTime? NextRetryAtUtc   { get; init; }
    public string?   LastError        { get; init; }
    public MessagePriority Priority   { get; init; } = MessagePriority.Normal;
    public string?   LockedByInstance { get; init; }
    public DateTime? LockedUntilUtc   { get; init; }
    public DateTime? UpdatedAtUtc     { get; init; }

    private OutboxMessage() { }

    public static OutboxMessage CreateNew(
        string type,
        string payload,
        IClock clock,
        MessagePriority priority = MessagePriority.Normal)
        => new()
        {
            Id             = Guid.NewGuid(),
            Type           = type,
            Payload        = payload,
            OccurredOnUtc  = clock.UtcNow,
            Priority       = priority,
            UpdatedAtUtc   = clock.UtcNow
        };

    public OutboxMessage MarkAsProcessed(IClock clock)
        => this with
        {
            Processed      = true,
            ProcessedOnUtc = clock.UtcNow,
            UpdatedAtUtc   = clock.UtcNow
        };

    public OutboxMessage IncrementRetry(IClock clock)
        => this with
        {
            RetryCount   = RetryCount + 1,
            UpdatedAtUtc = clock.UtcNow
        };

    public OutboxMessage SetNextRetry(DateTime nextRetryAtUtc, IClock clock)
        => this with
        {
            NextRetryAtUtc = nextRetryAtUtc,
            UpdatedAtUtc   = clock.UtcNow
        };

    public OutboxMessage SetLastError(string error, IClock clock)
        => this with
        {
            LastError    = error,
            UpdatedAtUtc = clock.UtcNow
        };

    public OutboxMessage SetPriority(MessagePriority priority, IClock clock)
        => this with
        {
            Priority     = priority,
            UpdatedAtUtc = clock.UtcNow
        };

    public OutboxMessage SetLockInfo(string instance, DateTime untilUtc, IClock clock)
        => this with
        {
            LockedByInstance = instance,
            LockedUntilUtc   = untilUtc,
            UpdatedAtUtc     = clock.UtcNow
        };

    public OutboxMessage ClearLockInfo(IClock clock)
        => this with
        {
            LockedByInstance = null,
            LockedUntilUtc   = null,
            UpdatedAtUtc     = clock.UtcNow
        };
}