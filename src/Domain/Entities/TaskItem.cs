using Domain.Enums;
using Domain.ValueObjects;
using Shared.Base;
using Shared.Exceptions;

namespace Domain.Entities;

public class TaskItem : BaseAuditableEntity
{
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public new DateTime CreatedDate { get; private set; }

    public DateRange? Schedule { get; private set; }
    public TaskPriority Priority { get; private set; } = TaskPriority.Medium;
    public TaskType Type { get; private set; } = TaskType.Task;

    public Guid ProjectId { get; private set; }
    public Guid? AssigneeId { get; private set; }
    public Guid? ReporterId { get; private set; }
    public Guid? ParentTaskId { get; private set; }
    public Guid? StatusId { get; private set; }

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

    protected TaskItem() { }

    public TaskItem(
        string title,
        string? description,
        TaskPriority priority,
        TaskType type,
        Guid projectId,
        Guid? assigneeId,
        Guid? reporterId)
    {
        SetTitle(title);
        Description = description;
        //TaskPriority mi olmalı?
        Priority = priority;
        Type = type;
        ProjectId = projectId;
        AssigneeId = assigneeId;
        ReporterId = reporterId;
        CreatedDate = DateTime.UtcNow;
    }

    public void SetSchedule(DateTime? start, DateTime? end)
    {
        if (start.HasValue && end.HasValue && end < start)
            throw new AppException("Validation.Task.Schedule", "End date cannot be earlier than start date.");

        Schedule = (start != null && end != null)
            ? DateRange.Create(start.Value, end.Value)
            : null;
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new AppException("Validation.Task.Title", "Task title is required.");
        Title = title;
    }

    public void AssignTo(Guid? assigneeId)
    {
        AssigneeId = assigneeId;
    }

    public void SetReporter(Guid? reporterId)
    {
        ReporterId = reporterId;
    }
    
    public void ChangePriority(TaskPriority priority)
    {
        Priority = priority;
    }
    
    public void ChangeType(TaskType type)
    {
        Type = type;
    }
    
}
