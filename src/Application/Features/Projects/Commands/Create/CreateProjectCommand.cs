namespace Application.Features.Projects.Commands.Create;

public record CreateProjectCommand(
    string Name,
    string? Description,
    DateTime StartDate,
    DateTime? EndDate,
    Guid ManagerId);