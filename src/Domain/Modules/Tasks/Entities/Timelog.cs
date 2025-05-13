using Domain.Modules.Users.Entities;
using Shared.Base;
using Shared.Constants;
using Shared.Validation;

namespace Domain.Modules.Tasks.Entities;

public class TimeLog : BaseAuditableEntity
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
        Guard.AgainstDefaultGuid(taskItemId, ErrorCodes.Validation, ValidationMessages.TimeLog.TaskIdRequired);
        Guard.AgainstDefaultGuid(userId, ErrorCodes.Validation, ValidationMessages.TimeLog.UserIdRequired);
        Guard.AgainstInvalidCondition(startTime >= endTime, ErrorCodes.Validation, ValidationMessages.TimeLog.StartTimeMustBeBeforeEndTime);

        TaskItemId = taskItemId;
        UserId = userId;
        StartTime = startTime;
        EndTime = endTime;
    }

    public void UpdateTime(DateTime newStart, DateTime newEnd)
    {
        Guard.AgainstInvalidCondition(newStart >= newEnd, ErrorCodes.Validation, ValidationMessages.TimeLog.StartTimeMustBeBeforeEndTime);
        StartTime = newStart;
        EndTime = newEnd;
    }
}