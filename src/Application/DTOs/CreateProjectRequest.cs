using Domain.Core.Primitives.Enums.Attributes;
using Domain.Modules.Projects.Enums;

namespace Application.DTOs;

public class CreateProjectRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid ManagerId { get; set; }
 
    [StructuredEnumName(typeof(ProjectStatus), allowNull: false)]
    public string Status { get; set; } = ProjectStatus.Planned.Name;

    [StructuredEnumName(typeof(ProjectPriority), allowNull: false)]
    public string Priority { get; set; } = ProjectPriority.Medium.Name;
}