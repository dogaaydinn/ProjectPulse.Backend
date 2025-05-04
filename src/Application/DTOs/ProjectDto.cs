namespace Application.DTOs;

public record ProjectDto(
    Guid Id,
    string Name,
    string? Description,
    DateTime StartDate,
    DateTime? EndDate,
    Guid ManagerId,
    string Status,
    string Priority
);