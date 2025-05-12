using Domain.Modules.Projects.Enums;

namespace Application.Features.Projects.Commands.Update;

public record UpdateProjectCommand(
    Guid Id,
    string Name,
    string? Description,
    DateTime StartDate,
    DateTime? EndDate,
    Guid ManagerId,
    ProjectStatus Status,
    ProjectPriority Priority
);