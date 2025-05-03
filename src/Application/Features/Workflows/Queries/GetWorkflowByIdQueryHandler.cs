using Application.DTOs;
using Domain.Repositories;
using Shared.Results;

namespace Application.Features.Workflows.Queries;

public class GetWorkflowByIdQueryHandler
{
    private readonly IWorkflowRepository _workflowRepository;

    public GetWorkflowByIdQueryHandler(IWorkflowRepository workflowRepository)
    {
        _workflowRepository = workflowRepository;
    }

    public async Task<Result<WorkflowDto>> Handle(GetWorkflowByIdQuery query, CancellationToken cancellationToken)
    {
        var workflow = await _workflowRepository.GetByIdAsync(query.Id);

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