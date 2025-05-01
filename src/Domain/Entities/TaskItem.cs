using Domain.Enums;
using Domain.ValueObjects;
using Shared.Base;

namespace Domain.Entities;

public class TaskItem : BaseAuditableEntity
{
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateRange? Schedule { get; private set; }
    public TaskPriority Priority { get; private set; } = TaskPriority.Medium;
    public TaskType Type { get; private set; } = TaskType.Task;

    // FK
    public Guid ProjectId { get; private set; }
    public Guid? AssigneeId { get; private set; }
    public Guid? ReporterId { get; private set; }
    public Guid? ParentTaskId { get; private set; }
    public Guid? StatusId { get; private set; }

    // Navigation
    public Project Project { get; private set; } = null!;
    public User? Assignee { get; private set; }
    public User? Reporter { get; private set; }
    public TaskItem? ParentTask { get; private set; }
    public TaskStatus? Status { get; private set; }

    public ICollection<TaskItem> SubTasks { get; private set; } = new List<TaskItem>();
    public ICollection<Comment> Comments { get; private set; } = new List<Comment>();
    public ICollection<TimeLog> TimeLogs { get; private set; } = new List<TimeLog>();
    public ICollection<Attachment> Attachments { get; private set; } = new List<Attachment>();
    public ICollection<TaskLabel> TaskLabels { get; private set; } = new List<TaskLabel>();
    public ICollection<TaskAssignment> TaskAssignments { get; private set; } = new List<TaskAssignment>();
    public ICollection<TaskDependency> Predecessors { get; private set; } = new List<TaskDependency>();
    public ICollection<TaskDependency> Successors { get; private set; } = new List<TaskDependency>();

    private TaskItem()
    {
    }

    public TaskItem(string title, Guid projectId, Guid? assigneeId, Guid? reporterId)
    {
        Title = title;
        CreatedDate = DateTime.UtcNow;
        ProjectId = projectId;
        AssigneeId = assigneeId;
        ReporterId = reporterId;
    }
    public void SetSchedule(DateTime? start, DateTime? end)
    {
        Schedule = (start != null && end != null)
            ? DateRange.Create(start.Value, end.Value)
            : null;
    }

}
