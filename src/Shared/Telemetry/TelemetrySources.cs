using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Shared.Telemetry;

public static class TelemetrySources
{
    private const string ServiceName = "ProjectPulse.Outbox";
    public static readonly ActivitySource OutboxActivity = new(ServiceName);
    public static readonly Meter OutboxMeter     = new(ServiceName);
}