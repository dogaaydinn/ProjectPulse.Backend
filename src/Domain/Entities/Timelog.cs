using Shared.Base;

namespace Domain.Entities;

public class TimeLog : BaseEntity
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; } = null!;

    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public TimeSpan Duration => EndTime - StartTime;

    public string? Description { get; private set; }
    public DateTime LoggedAt { get; private set; }

    private TimeLog() { }

    public TimeLog(Guid userId, Guid taskItemId, DateTime startTime, DateTime endTime, string? description)
    {
        if (endTime <= startTime)
            throw new ArgumentException("EndTime must be after StartTime");

        UserId = userId;
        TaskItemId = taskItemId;
        StartTime = startTime;
        EndTime = endTime;
        Description = description;
        LoggedAt = DateTime.UtcNow;
    }

    public void UpdateTimes(DateTime start, DateTime end)
    {
        if (end <= start)
            throw new ArgumentException("EndTime must be after StartTime");

        StartTime = start;
        EndTime = end;
    }

    public void UpdateDescription(string? description)
    {
        Description = description;
    }
}