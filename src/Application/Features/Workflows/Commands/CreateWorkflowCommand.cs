namespace Application.Features.Workflows.Commands;

public record CreateWorkflowCommand(
    string Name,
    Guid ProjectId,
    bool IsDefault
);