using Domain.Enums;
using Domain.Primitives.Enums.StructuredEnum;

namespace Application.DTOs;

public class UpdateProjectRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid ManagerId { get; set; }
    [StructuredEnumName(typeof(ProjectStatus), allowNull: false)]
    public string Status { get; set; } = ProjectStatus.Planned.Name;

    [StructuredEnumName(typeof(ProjectPriority), allowNull: false)]
    public string Priority { get; set; } = ProjectPriority.Normal.Name;
}
