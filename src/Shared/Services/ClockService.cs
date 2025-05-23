using Shared.Time;

namespace Shared.Services;

public class ClockService : IClockService
{
    private readonly IClock _clock;

    public ClockService(IClock clock)
    {
        _clock = clock;
    }

    public DateTime Now => _clock.UtcNow;
    public DateTimeOffset NowOffset => _clock.UtcNowOffset;
    public DateOnly Today => _clock.Today;

    public bool IsWeekend() => _clock.Today.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
    public bool IsBeforeNow(DateTime dt) => dt < _clock.UtcNow;
    public bool IsAfterNow(DateTime dt) => dt > _clock.UtcNow;

    public TimeSpan TimeUntil(DateTime target) => target - _clock.UtcNow;
    public TimeSpan TimeSince(DateTime past) => _clock.UtcNow - past;
}