namespace Shared.Time;

public class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTimeOffset UtcNowOffset => DateTimeOffset.UtcNow;
    public DateOnly Today => DateOnly.FromDateTime(UtcNow); 
    public TimeProvider Provider => TimeProvider.System;
    public void Advance(TimeSpan duration) => throw new NotImplementedException();
    public void SetTime(DateTime newTime) => throw new NotImplementedException();
}