namespace Shared.Time;

public interface IClock
{
    DateTime UtcNow { get; }
    DateTimeOffset UtcNowOffset { get; }
    DateOnly Today { get; } 
    TimeProvider Provider { get; }
    void Advance(TimeSpan duration);
    void SetTime(DateTime newTime);
}