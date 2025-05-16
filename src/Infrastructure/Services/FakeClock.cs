// TestClock için zaman atlama özelliği

using Shared.Time;

public class FakeClock : IClock
{
    private DateTime _currentTime;
    
    public TestClock(DateTime initialTime) => _currentTime = initialTime;
    
    public DateTime UtcNow => _currentTime;
    
    public void Advance(TimeSpan duration) => _currentTime += duration;
    
    public void Set(DateTime newTime) => _currentTime = newTime;
}