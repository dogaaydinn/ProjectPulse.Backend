using Domain.Enums;

namespace Application.Features.Tasks.Commands.Create;

public record CreateTaskCommand(
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