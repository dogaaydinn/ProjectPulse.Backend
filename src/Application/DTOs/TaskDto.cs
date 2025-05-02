namespace Application.DTOs;

public record TaskDto(
    Guid Id,
    string Title,
    string? Description,
    string Priority,
    string Type,
    Guid ProjectId,
    Guid? AssigneeId,
    Guid? ReporterId,
    DateTime? StartDate,
    DateTime? EndDate
);