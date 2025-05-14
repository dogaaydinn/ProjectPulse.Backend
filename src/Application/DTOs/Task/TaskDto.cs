using Shared.ValueObjects;

namespace Application.DTOs.Task;

public record TaskDto(
    Guid Id,
    LocalizedString Title,
    LocalizedString? Description,
    
    
    string TaskPriority,
    string TaskType,
    Guid ProjectId,
    Guid? AssigneeId,
    Guid? ReporterId,
    DateTime? StartDate,
    DateTime? EndDate
);