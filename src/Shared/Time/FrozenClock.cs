namespace Shared.Time;

public sealed class FrozenClock : IClock
{
    private readonly DateTime _frozenTime;

    public FrozenClock(DateTime freezeTime) => _frozenTime = freezeTime;

    public DateTime UtcNow => _frozenTime;
    public DateTimeOffset UtcNowOffset => new(_frozenTime);
    public TimeProvider Provider => new FrozenTimeProvider(_frozenTime);

    public void Advance(TimeSpan duration) => throw new NotSupportedException("Frozen clock cannot be advanced");
    public void SetTime(DateTime newTime) => throw new NotSupportedException("Frozen clock cannot be set");

    private class FrozenTimeProvider(DateTime time) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => time;
        public override long GetTimestamp() => time.Ticks;
        public override TimeZoneInfo LocalTimeZone => TimeZoneInfo.Utc;
    }
}