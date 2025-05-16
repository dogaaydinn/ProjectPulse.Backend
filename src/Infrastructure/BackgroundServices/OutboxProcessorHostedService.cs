// Infrastructure/BackgroundServices/OutboxProcessorHostedService.cs
using System.Text.Json;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Abstractions.Messaging;
using Shared.Events;
using Shared.Messaging.Contracts;

namespace Infrastructure.BackgroundServices;

public sealed class OutboxProcessorHostedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OutboxProcessorHostedService> _logger;
    private readonly TimeSpan _interval;
    private readonly JsonSerializerOptions _serializerOptions;
    private const int MaxRetryAttempts = 3;

    public OutboxProcessorHostedService(
        IServiceProvider serviceProvider,
        ILogger<OutboxProcessorHostedService> logger,
        IOptions<OutboxOptions> options,
        IOptions<JsonSerializerOptions>? serializerOptions = null)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _interval = options.Value.ProcessingInterval;
        _serializerOptions = serializerOptions?.Value ?? new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(_interval);

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();

            var messages = await dbContext.Set<OutboxMessage>()
                .Where(m => !m.Processed)
                .OrderBy(m => m.OccurredOnUtc)
                .Take(100)
                .ToListAsync(stoppingToken);

            foreach (var message in messages)
            {
                await ProcessWithRetryAsync(message, messageBus, stoppingToken);
            }

            await dbContext.SaveChangesAsync(stoppingToken);
        }
    }

    private async Task ProcessWithRetryAsync(
        OutboxMessage message,
        IMessageBus messageBus,
        AppDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var retryCount = 0;

        while (retryCount < MaxRetryAttempts)
        {
            try
            {
                var eventType = Type.GetType(message.Type);
                if (eventType is null) return;

                var domainEvent = JsonSerializer.Deserialize(
                    message.Payload, eventType, _serializerOptions) as IDomainEvent;

                if (domainEvent is not null)
                {
                    await messageBus.PublishAsync(domainEvent, cancellationToken);
                    message.MarkAsProcessed();
                }

                return;
            }
            catch (Exception ex)
            {
                retryCount++;
                var delay = TimeSpan.FromSeconds(Math.Pow(2, retryCount));

                _logger.LogWarning(ex,
                    "Retry {RetryCount} for message {MessageId}. Waiting {Delay}ms",
                    retryCount, message.Id, delay.TotalMilliseconds);

                await Task.Delay(delay, cancellationToken);
            }
        }

        // ❗ MaxRetry aşıldıysa DLQ'ya taşı
        _logger.LogError("Message {MessageId} failed after {MaxRetries} attempts", message.Id, MaxRetryAttempts);
        await MoveToDeadLetterQueue(message, dbContext);
    }


public class OutboxOptions
{
    public TimeSpan ProcessingInterval { get; set; } = TimeSpan.FromSeconds(30);
    // OutboxOptions'a ekleyin
    public int BatchSize { get; set; } = 100;
    public int MaxRetryCount { get; set; } = 3;

// Process metodunda
    if (message.RetryCount >= _options.MaxRetryCount)
    {
        await MoveToDeadLetterQueue(message);
        continue;
    }
}
