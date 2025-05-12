namespace Shared.Time;

public interface IClock
{
    DateTime UtcNow { get; }
}