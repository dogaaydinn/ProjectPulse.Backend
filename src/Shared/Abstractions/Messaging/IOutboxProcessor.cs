using Shared.Messaging.Contracts;

namespace Shared.Abstractions.Messaging;

public interface IOutboxProcessor
{
    Task ProcessPendingMessagesAsync();
    Task ScheduleRetryAsync(OutboxMessage message);
}