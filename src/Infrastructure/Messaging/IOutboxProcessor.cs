using Shared.Messaging.Contracts;

namespace Infrastructure.Messaging;

public interface IOutboxProcessor
{
    Task ProcessPendingMessagesAsync();
    Task ScheduleRetryAsync(OutboxMessage message);
}