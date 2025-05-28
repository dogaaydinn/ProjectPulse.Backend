namespace Shared.Time;

public sealed class FrozenClock : IClock
{
    private DateTime _frozenTime;

    public FrozenClock(DateTime freezeTime) =>
        _frozenTime = freezeTime;

    public DateTime UtcNow => _frozenTime;
    public DateTimeOffset UtcNowOffset => new(_frozenTime);
    public DateOnly Today => DateOnly.FromDateTime(UtcNow); 
    public TimeProvider Provider => new FrozenTimeProvider(_frozenTime);

    public void Advance(TimeSpan _) =>
        throw new NotSupportedException("Frozen clock cannot be advanced");

    public void SetTime(DateTime newTime) =>
        _frozenTime = newTime;

    private class FrozenTimeProvider : TimeProvider
    {
        private readonly DateTime _time;
        public FrozenTimeProvider(DateTime time) => _time = time;
        public override DateTimeOffset GetUtcNow() => _time;
        public override long GetTimestamp() => _time.Ticks;
        public override TimeZoneInfo LocalTimeZone => TimeZoneInfo.Utc;
    }
}