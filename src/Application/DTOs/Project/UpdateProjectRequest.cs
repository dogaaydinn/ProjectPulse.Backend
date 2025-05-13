using Domain.Core.Primitives.Enums.Attributes;
using Domain.Modules.Projects.Enums;
using Shared.ValueObjects;

namespace Application.DTOs.Project;

public class UpdateProjectRequest
{
    public Guid Id { get; set; }
    public LocalizedString Name { get; set; }
    public LocalizedString Description { get; set; } 
    public DateRange Schedule { get; set; } 
    public Guid ManagerId { get; set; }
    [StructuredEnumName(typeof(ProjectStatus), allowNull: false)]
    public string Status { get; set; } = ProjectStatus.Planned.Name;

    [StructuredEnumName(typeof(ProjectPriority), allowNull: false)]
    public string Priority { get; set; } = ProjectPriority.Medium.Name;
}