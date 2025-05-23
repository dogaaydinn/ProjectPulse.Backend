namespace Shared.Time;

public interface IClock
{
    DateTime UtcNow { get; }
    DateTimeOffset UtcNowOffset { get; }
    DateOnly Today => DateOnly.FromDateTime(UtcNow);
    TimeProvider Provider { get; }
    void Advance(TimeSpan duration);
    void SetTime(DateTime newTime);
}