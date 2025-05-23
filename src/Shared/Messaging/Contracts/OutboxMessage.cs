using Shared.Time;

namespace Shared.Messaging.Contracts;

public sealed class OutboxMessage
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Type { get; init; } = string.Empty;
    public string Payload { get; init; } = string.Empty;
    public DateTime OccurredOnUtc { get; init; } 
    public OutboxMessage(string type, string payload, IClock clock)
    {
        Id = Guid.NewGuid();
        Type = type;
        Payload = payload;
        OccurredOnUtc = clock.UtcNow;
    }
    public bool Processed { get; set; } = false;
    public DateTime? ProcessedOnUtc { get; set; }
    public int RetryCount { get; set; } = 0;
}