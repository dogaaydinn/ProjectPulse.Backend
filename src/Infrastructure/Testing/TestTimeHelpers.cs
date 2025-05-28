using Microsoft.Extensions.DependencyInjection;
using Shared.Time;

namespace Infrastructure.Testing;

public static class TestTimeHelpers
{
    private static TestClock CreateTestClock(DateTime? initialTime = null) =>
        new(initialTime ?? DateTime.UtcNow);

    public static IServiceCollection ReplaceWithTestClock(this IServiceCollection services)
    {
        var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(IClock));
        if (descriptor is not null)
            services.Remove(descriptor);
        
        var testClock = CreateTestClock();
        services.AddSingleton<IClock>(testClock);
        services.AddSingleton(testClock.Provider);

        return services;
    }
}