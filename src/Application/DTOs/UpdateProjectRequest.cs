using Domain.Enums;

namespace Application.DTOs;

public class UpdateProjectRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid ManagerId { get; set; }
    public ProjectStatus Status { get; set; }
    public ProjectPriority Priority { get; set; }
}