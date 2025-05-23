using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Telemetry;

public static class AppInsightsSetup
{
    public static IServiceCollection AddAppInsights(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new ApplicationInsightsServiceOptions();
        configuration.Bind("ApplicationInsights", options);

        services.AddApplicationInsightsTelemetry(options);
        services.AddSingleton<TelemetryClient>();

        return services;
    }
}