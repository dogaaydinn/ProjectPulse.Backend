using Domain.Modules.Users.Entities;
using Shared.Base;
using Shared.Exceptions;

namespace Domain.Modules.Tasks.Entities;

public class TimeLog : BaseEntity
{
    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; } = null!;

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    public TimeSpan Duration => EndTime - StartTime;

    protected TimeLog() { }

    internal TimeLog(Guid taskItemId, Guid userId, DateTime startTime, DateTime endTime)
    {
        if (taskItemId == Guid.Empty || userId == Guid.Empty)
            throw new AppException("Validation.TimeLog.InvalidIds", "Task and User IDs must be valid.");

        if (startTime >= endTime)
            throw new AppException("Validation.TimeLog.InvalidTimes", "Start time must be before end time.");

        TaskItemId = taskItemId;
        UserId = userId;
        StartTime = startTime;
        EndTime = endTime;
    }

    public void UpdateTime(DateTime newStart, DateTime newEnd)
    {
        if (newStart >= newEnd)
            throw new AppException("Validation.TimeLog.InvalidTimes", "Start time must be before end time.");

        StartTime = newStart;
        EndTime = newEnd;
    }
}