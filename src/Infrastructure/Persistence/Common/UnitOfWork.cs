using Domain.Core.Persistence;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Common;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}