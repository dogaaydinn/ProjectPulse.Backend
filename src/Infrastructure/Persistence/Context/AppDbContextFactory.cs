using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Shared.Time;

namespace Infrastructure.Persistence.Context;

public class AppDbContextFactory : IDbContextFactory<AppDbContext>
{
    private readonly IClock _clock;
    
    public AppDbContextFactory(IClock clock)
    {
        _clock = clock;
    }

    public AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .AddInterceptors(new TimeStampInterceptor(_clock))
            .UseSqlServer(/* connection string */)
            .Options;

        return new AppDbContext(options);
    }
}