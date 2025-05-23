namespace Shared.Time;

public sealed class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTimeOffset UtcNowOffset => DateTimeOffset.UtcNow;
    public TimeProvider Provider => TimeProvider.System;

    public void Advance(TimeSpan _) =>
        throw new NotSupportedException("System clock cannot be advanced");

    public void SetTime(DateTime _) =>
        throw new NotSupportedException("System clock cannot be set");
}