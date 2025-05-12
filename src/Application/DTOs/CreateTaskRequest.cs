using Domain.Core.Primitives.Enums.Attributes;
using Domain.Modules.Tasks.Enums;

namespace Application.DTOs;

public class CreateTaskRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    [StructuredEnumName(typeof(TaskPriority), allowNull: false)]
    public string Priority { get; set; } = TaskPriority.Medium.Name;
    [StructuredEnumName(typeof(TaskType), allowNull: false)]
    public string Type { get; set; } = TaskType.Task.Name;
    public Guid ProjectId { get; set; }
    public Guid? AssigneeId { get; set; }
    public Guid? ReporterId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}