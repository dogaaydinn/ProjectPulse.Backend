using Application.DTOs;
using Application.DTOs.Workflow;
using Domain.Modules.Projects.Repositories;
using Shared.Results;

namespace Application.Features.Workflows.Queries;

public class GetWorkflowsByProjectIdQueryHandler(IWorkflowRepository workflowRepository)
{
    public async Task<Result<List<WorkflowDto>>> Handle(GetWorkflowsByProjectIdQuery query, CancellationToken cancellationToken)
    {
        var workflows = await workflowRepository.GetByProjectIdAsync(query.ProjectId);

        var dtos = workflows
            .Select(w => new WorkflowDto(
                w.Id,
                w.Name,
                w.ProjectId,
                w.IsDefault))
            .ToList();

        return Result<List<WorkflowDto>>.Success(dtos);
    }
}