// Infrastructure/DependencyInjection/InfrastructureServicesRegistration.cs

using Domain.Core.Persistence;
using Infrastructure.Caching;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.Context;
using Infrastructure.Services.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Caching;
using StackExchange.Redis;

namespace Infrastructure.DependencyInjection;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddCaching(configuration)
            .AddBackgroundProcessing(configuration)
            .AddDistributedCaching(configuration)
            .AddJwtAuthentication(configuration)
            .AddDomainEvents()
            .AddFileStorage(configuration)
            .AddHealthChecks(configuration);

        return services;
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("Database"),
                npgsql => npgsql
                    .EnableRetryOnFailure()
                    .MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());
        
        services.AddScoped<BaseSpecificationEvaluator>();
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

        return services;
    }

    private static IServiceCollection AddCaching(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "ProjectPulse_";
        });

        services.AddSingleton<IConnectionMultiplexer>(_ => 
            ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));

        services.AddSingleton<ICacheService, RedisCacheService>();

        return services;
    }

    private static IServiceCollection AddDomainEvents(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        
        services.Scan(scan => scan
            .FromAssemblies(typeof(InfrastructureServicesRegistration).Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}