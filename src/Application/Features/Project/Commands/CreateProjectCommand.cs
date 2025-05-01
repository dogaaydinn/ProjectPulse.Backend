namespace Application.Features.Project.Commands;

public record CreateProjectCommand(
    string Name,
    string? Description,
    DateTime StartDate,
    DateTime? EndDate,
    Guid ManagerId
);