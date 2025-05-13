namespace Application.DTOs.Task;

public record CreateTaskCommand(
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