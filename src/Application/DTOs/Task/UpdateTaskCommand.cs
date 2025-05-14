using Shared.ValueObjects;

namespace Application.DTOs.Task;

public record UpdateTaskCommand(
    Guid Id,
    LocalizedString Title,
    
    string Priority,
    string Type,
    Guid ProjectId,
    Guid? AssigneeId,
    Guid? ReporterId,
    DateTime? StartDate,
    DateTime? EndDate
);