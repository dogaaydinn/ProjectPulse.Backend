using Domain.Enums;
using Domain.Primitives.Enums.StructuredEnum;

namespace Application.DTOs;

public class CreateTaskRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    [StructuredEnumName(typeof(TaskPriority), allowNull: false)]
    public string Priority { get; set; } = "Medium";
    public string Type { get; set; } = "Task";
    public Guid ProjectId { get; set; }
    public Guid? AssigneeId { get; set; }
    public Guid? ReporterId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}