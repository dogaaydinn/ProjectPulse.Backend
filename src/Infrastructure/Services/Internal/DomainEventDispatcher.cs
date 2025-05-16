using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Events;

namespace Infrastructure.Services.Internal;

public sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DomainEventDispatcher> _logger;

    public DomainEventDispatcher(IServiceProvider serviceProvider, ILogger<DomainEventDispatcher> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken ct = default)
    {
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
        using var scope = _serviceProvider.CreateScope();

        try
        {
            var handlers = scope.ServiceProvider.GetServices(handlerType);

            var tasks = handlers.Select(handler =>
                (Task)handler.GetType()
                    .GetMethod("HandleAsync")!
                    .Invoke(handler, new object[] { domainEvent, ct })!);

            await Task.WhenAll(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error dispatching domain event {EventType}", domainEvent.GetType().Name);
            throw;
        }
    }
    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            await DispatchAsync(domainEvent, ct);
        }
    }

}