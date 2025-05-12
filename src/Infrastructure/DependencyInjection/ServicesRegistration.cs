using Microsoft.Extensions.DependencyInjection;
using Shared.Time;

namespace Infrastructure.DependencyInjection;

public static class ServicesRegistration
{
    public static IServiceCollection AddOtherInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IClock, SystemClock>();
        // services.AddScoped<IEmailSender, SmtpEmailSender>();
        // services.AddSingleton<ICacheService, RedisCacheService>();

        return services;
    }
}