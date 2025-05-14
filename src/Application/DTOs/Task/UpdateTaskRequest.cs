using Domain.Core.Primitives.Enums.Attributes;
using Domain.Modules.Tasks.Enums;

namespace Application.DTOs.Task;

public class UpdateTaskRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    
    [StructuredEnumName(typeof(TaskPriority), allowNull: false)]
    public string Priority { get; set; } = TaskPriority.Medium.Name;

    [StructuredEnumName(typeof(TaskType), allowNull: false)]
    public string Type { get; set; } = TaskType.Feature.Name;

    public Guid ProjectId { get; set; }
    public Guid? AssigneeId { get; set; }
    public Guid? ReporterId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}