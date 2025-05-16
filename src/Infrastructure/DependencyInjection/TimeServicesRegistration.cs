using Microsoft.Extensions.DependencyInjection;
using Shared.Time;

namespace Infrastructure.DependencyInjection;

public static class TimeServicesRegistration
{
    public static IServiceCollection AddTimeServices(this IServiceCollection services)
    {
        // Production'da SystemClock, testlerde TestClock kullanılacak
        services.AddSingleton<IClock, SystemClock>();
        
        // Background servisler için TimeProvider erişimi
        services.AddSingleton(provider => provider.GetRequiredService<IClock>().Provider);
        
        return services;
    }
}