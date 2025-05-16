// Infrastructure/Monitoring/HealthCheckService.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;

namespace Infrastructure.Monitoring;

public class RedisHealthCheck : IHealthCheck
{
    private readonly IConnectionMultiplexer _redis;

    public RedisHealthCheck(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            foreach (var endpoint in _redis.GetEndPoints())
            {
                var server = _redis.GetServer(endpoint);
                if (server.ServerType == ServerType.Cluster)
                {
                    await _redis.GetDatabase().ExecuteAsync("CLUSTER", "INFO");
                }
                else
                {
                    await _redis.GetDatabase().PingAsync();
                }
            }

            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(
                description: "Redis is unavailable",
                exception: ex);
        }
    }
}

public class DatabaseHealthCheck<TContext> : IHealthCheck 
    where TContext : DbContext
{
    private readonly TContext _dbContext;

    public DatabaseHealthCheck(TContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbContext.Database.CanConnectAsync(cancellationToken);
            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(
                description: "Database is unavailable",
                exception: ex);
        }
    }
}