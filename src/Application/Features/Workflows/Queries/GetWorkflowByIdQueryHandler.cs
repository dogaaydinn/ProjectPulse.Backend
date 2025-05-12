using Application.DTOs;
using Domain.Modules.Projects.Repositories;
using Shared.Results;

namespace Application.Features.Workflows.Queries;

public class GetWorkflowByIdQueryHandler(IWorkflowRepository workflowRepository)
{
    public async Task<Result<WorkflowDto>> Handle(GetWorkflowByIdQuery query, CancellationToken cancellationToken)
    {
        var workflow = await workflowRepository.GetByIdAsync(query.Id);

        if (workflow is null)
            return Result<WorkflowDto>.Failure(Error.NotFound("Workflow", query.Id));

        var dto = new WorkflowDto(
            workflow.Id,
            workflow.Name,
            workflow.ProjectId,
            workflow.IsDefault
        );

        return Result<WorkflowDto>.Success(dto);
    }
}