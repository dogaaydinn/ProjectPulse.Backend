using Domain.Enums;

namespace Application.Features.Projects.Commands.Create;

public record CreateCommand(
    string Title,
    string? Description,
    TaskPriority Priority,
    TaskType Type,
    Guid ProjectId,
    Guid? AssigneeId,
    Guid? ReporterId,
    DateTime? StartDate,
    DateTime? EndDate
);