// Shared/Messaging/Contracts/OutboxMessage.cs
namespace Shared.Messaging.Contracts;

public sealed class OutboxMessage
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Type { get; init; } = string.Empty;
    public string Payload { get; init; } = string.Empty;
    public DateTime OccurredOnUtc { get; init; } = DateTime.UtcNow;
    public bool Processed { get; set; } = false;
    public DateTime? ProcessedOnUtc { get; set; }
    public int RetryCount { get; set; } = 0;
}