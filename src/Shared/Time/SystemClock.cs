namespace Shared.Time;

public class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}