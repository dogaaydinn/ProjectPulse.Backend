namespace Shared.Time;

public sealed class TestClock : IClock
{
    private DateTime _utcNow;

    public TestClock(DateTime? initialTime = null)
    {
        _utcNow = initialTime ?? DateTime.UtcNow;
    }

    public DateTime UtcNow => _utcNow;
    public DateTimeOffset UtcNowOffset => new(_utcNow);
    public TimeProvider Provider => new TestTimeProvider(_utcNow);

    public void Advance(TimeSpan duration) => _utcNow += duration;
    public void SetTime(DateTime newTime) => _utcNow = newTime;

    private class TestTimeProvider(DateTime initialTime) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => initialTime;
        public override long GetTimestamp() => initialTime.Ticks;
        public override TimeZoneInfo LocalTimeZone => TimeZoneInfo.Utc;
    }
}