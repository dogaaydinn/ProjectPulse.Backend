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
    public DateOnly Today => DateOnly.FromDateTime(UtcNow); 
    public TimeProvider Provider => new TestTimeProvider(_utcNow);

    public void Advance(TimeSpan duration) =>
        _utcNow = _utcNow.Add(duration);

    public void SetTime(DateTime newTime) =>
        _utcNow = newTime;

    private class TestTimeProvider : TimeProvider
    {
        private readonly DateTime _time;
        public TestTimeProvider(DateTime time) => _time = time;
        public override DateTimeOffset GetUtcNow() => _time;
        public override long GetTimestamp() => _time.Ticks;
        public override TimeZoneInfo LocalTimeZone => TimeZoneInfo.Utc;
    }
}