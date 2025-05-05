namespace Application.DTOs;

public class UpdateTaskRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    
    public string Priority { get; set; } = "Medium";
    public string Type { get; set; } = "Task";
    public Guid ProjectId { get; set; }
    public Guid? AssigneeId { get; set; }
    public Guid? ReporterId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}