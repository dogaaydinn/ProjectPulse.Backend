using Domain.Enums;

namespace Application.Features.Tasks.Commands;

public record CreateTaskCommand(
    string Title,
    string? Description,
    Priority Priority,
    TaskType Type,
    Guid ProjectId,
    Guid? AssigneeId,
    Guid? ReporterId,
    DateTime? StartDate,
    DateTime? EndDate
);