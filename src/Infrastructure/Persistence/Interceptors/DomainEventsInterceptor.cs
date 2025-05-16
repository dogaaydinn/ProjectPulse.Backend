using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Shared.Base;

namespace Infrastructure.Persistence.Interceptors;

public sealed class DomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IDomainEventDispatcher _dispatcher;
    private readonly ILogger<DomainEventsInterceptor> _logger;

    public DomainEventsInterceptor(IDomainEventDispatcher dispatcher, ILogger<DomainEventsInterceptor> logger)
    {
        _dispatcher = dispatcher;
        _logger = logger;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context is null) return result;

        var domainEvents = context.ChangeTracker
            .Entries<IAggregateRoot<>>()
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            try
            {
                await _dispatcher.DispatchAsync(domainEvent, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error dispatching domain event {EventType}", domainEvent.GetType().Name);
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}