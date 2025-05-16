using Microsoft.Extensions.Logging;
using Shared.Messaging.Contracts;

namespace Infrastructure.Messaging;

public sealed class OutboxProcessor : IOutboxProcessor
{
    private readonly ILogger<OutboxProcessor> _logger;

    public OutboxProcessor(ILogger<OutboxProcessor> logger)
    {
        _logger = logger;
    }

    public Task ProcessPendingMessagesAsync()
    {
        // TODO: Load unprocessed OutboxMessages from DB
        // TODO: Publish via MessageBus or IntegrationEventService
        _logger.LogInformation("Processing pending outbox messages...");
        return Task.CompletedTask;
    }

    public Task ScheduleRetryAsync(OutboxMessage message)
    {
        // TODO: Set next retry time or mark for retry
        _logger.LogWarning("Retry scheduled for message: {MessageId}", message.Id);
        return Task.CompletedTask;
    }
}