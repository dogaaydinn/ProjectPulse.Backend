namespace Application.DTOs.Workflow;

public record WorkflowDto(
    Guid Id,
    string Name,
    Guid ProjectId,
    bool IsDefault
);