namespace Shared.Services;

public interface IClockService
{
    DateTime Now { get; }
    DateTimeOffset NowOffset { get; }
    DateOnly Today { get; }

    bool IsWeekend();
    bool IsBeforeNow(DateTime dt);
    bool IsAfterNow(DateTime dt);

    TimeSpan TimeUntil(DateTime target);
    TimeSpan TimeSince(DateTime past);
}