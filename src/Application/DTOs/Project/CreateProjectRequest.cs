using Application.DTOs.Common;
using Application.DTOs.Project.Interfaces;
using Domain.Modules.Projects.Enums;

namespace Application.DTOs.Project;

public class CreateProjectRequest : IProjectCreateRequest
{
    public required LocalizedStringDto Name { get; init; }
    public LocalizedStringDto? Description { get; init; }
    public required DateRangeDto Schedule { get; init; }
    public required Guid ManagerId { get; init; }
    public string Status { get; set; } = ProjectStatus.Planned.Name;
    public string Priority { get; set; } = ProjectPriority.Medium.Name;
}