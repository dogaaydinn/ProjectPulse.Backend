using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Shared.Base;
using Shared.Services;
using Shared.Time;

namespace Infrastructure.Persistence.Interceptors;

public sealed class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly IClock _clock;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<AuditableEntityInterceptor> _logger;


    public AuditableEntityInterceptor(
        IClock clock,
        ICurrentUserService currentUserService, ILogger<AuditableEntityInterceptor> logger)
    {
        _clock = clock;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context is null)
            return result;

        UpdateAuditableEntities(context);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    

    private void UpdateAuditableEntities(DbContext context)
    {
        var entries = context.ChangeTracker
            .Entries<IAuditableEntity>();

        foreach (var entry in entries)
        {
            try
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOnUtc = _clock.UtcNow;
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                }

                if (entry.State == EntityState.Modified ||
                    entry.State == EntityState.Deleted)
                {
                    entry.Entity.LastModifiedOnUtc = _clock.UtcNow;
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Auditable entity could not be timestamped for entity {EntityType}.", entry.Entity.GetType().Name);
            }
        }
    }
}