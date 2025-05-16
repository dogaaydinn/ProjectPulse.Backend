using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.Base;
using Shared.Time;

namespace Infrastructure.Persistence.Interceptors;

public sealed class TimeStampInterceptor : SaveChangesInterceptor
{
    private readonly IClock _clock;

    public TimeStampInterceptor(IClock clock)
    {
        _clock = clock;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        UpdateTimestamps(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateTimestamps(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateTimestamps(DbContext? context)
    {
        if (context is null) return;

        var entries = context.ChangeTracker.Entries<IAuditableEntity>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedOnUtc = _clock.UtcNow;
            }

            if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
            {
                entry.Entity.LastModifiedOnUtc = _clock.UtcNow;
            }
        }
    }
}