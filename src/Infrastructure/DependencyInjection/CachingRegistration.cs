using Infrastructure.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Caching;
using StackExchange.Redis;

namespace Infrastructure.DependencyInjection;

public static class CachingRegistration
{
    public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration config)
    {
        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = config.GetConnectionString("Redis")!;
        });

        services.AddSingleton<IConnectionMultiplexer>(
            _ => ConnectionMultiplexer.Connect(config.GetConnectionString("Redis")));

        services.AddSingleton<ICacheService, RedisCacheService>();

        return services;
    }
}