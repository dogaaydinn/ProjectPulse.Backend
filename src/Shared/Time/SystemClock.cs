namespace Shared.Time;

public sealed class SystemClock : IClock
{
    // Real-time, immutable
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTimeOffset UtcNowOffset => DateTimeOffset.UtcNow;
    public TimeProvider Provider => TimeProvider.System;

    public void Advance(TimeSpan _) => throw new NotSupportedException();
    public void SetTime(DateTime _) => throw new NotSupportedException();
}