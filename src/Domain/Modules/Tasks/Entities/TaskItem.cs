using Domain.Modules.Projects.Entities;
using Domain.Modules.Tasks.Enums;
using Domain.Modules.Users.Entities;
using Shared.Base;
using Shared.Constants;
using Shared.Validation;
using Shared.ValueObjects;

namespace Domain.Modules.Tasks.Entities;

public class TaskItem : BaseAuditableEntity
{
    public LocalizedString Title { get; private set; } = null!;
    public LocalizedString? Description { get; private set; }
    public DateRange? Schedule { get; private set; }
    public TaskPriority Priority { get; private set; } = TaskPriority.Medium;
    public TaskType Type { get; private set; } = TaskType.Task;

    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; } = null!;

    public Guid? AssigneeId { get; private set; }
    public User? Assignee { get; private set; }

    public Guid? ReporterId { get; private set; }
    public User? Reporter { get; private set; }

    public Guid? ParentTaskId { get; private set; }
    public TaskItem? ParentTask { get; private set; }

    public Guid? StatusId { get; private set; }
    public TaskStatus? Status { get; private set; }

    public ICollection<TaskItem> SubTasks { get; private set; } = new List<TaskItem>();
    public ICollection<Comment> Comments { get; private set; } = new List<Comment>();
    public ICollection<TimeLog> TimeLogs { get; private set; } = new List<TimeLog>();
    public ICollection<Attachment> Attachments { get; private set; } = new List<Attachment>();
    public ICollection<TaskLabel> TaskLabels { get; private set; } = new List<TaskLabel>();
    public ICollection<TaskAssignment> TaskAssignments { get; private set; } = new List<TaskAssignment>();
    public ICollection<TaskDependency> Predecessors { get; private set; } = new List<TaskDependency>();
    public ICollection<TaskDependency> Successors { get; private set; } = new List<TaskDependency>();

    protected TaskItem() { }

    public TaskItem(
        LocalizedString title,
        LocalizedString? description,
        TaskPriority priority,
        TaskType type,
        Guid projectId,
        Guid? assigneeId,
        Guid? reporterId)
    {
        SetTitle(title);
        Description = description;
        Priority = priority;
        Type = type;

        Guard.AgainstDefaultGuid(projectId, ErrorCodes.Validation, ValidationMessages.Common.ProjectIdRequired);
        ProjectId = projectId;

        AssigneeId = assigneeId;
        ReporterId = reporterId;
    }

    public void SetTitle(LocalizedString title)
    {
        Guard.AgainstEmptyLocalized(title, ErrorCodes.Validation, ValidationMessages.Task.TitleRequired);
        Title = title;
    }

    public void SetDescription(LocalizedString? description)
    {
        Description = description;
    }

    public void SetSchedule(DateRange? schedule)
    {
        Schedule = schedule;
    }

    public void UpdateDetails(
        LocalizedString title,
        LocalizedString? description,
        TaskPriority priority,
        TaskType type,
        DateRange? schedule)
    {
        SetTitle(title);
        SetDescription(description);
        Priority = priority;
        Type = type;
        SetSchedule(schedule);
    }

    public void AssignTo(Guid? assigneeId) => AssigneeId = assigneeId;
    public void SetReporter(Guid? reporterId) => ReporterId = reporterId;
    public void ChangePriority(TaskPriority priority) => Priority = priority;
    public void ChangeType(TaskType type) => Type = type;
}
