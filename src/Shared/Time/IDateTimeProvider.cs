namespace Shared.Time;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
public class SystemDateTimeProvider : IDateTimeProvider 
{
    public DateTime UtcNow => DateTime.UtcNow;
}