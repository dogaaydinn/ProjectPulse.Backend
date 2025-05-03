using Application.DTOs;
using Domain.Repositories;
using Shared.Results;

namespace Application.Features.Workflows.Queries;

public class GetWorkflowsByProjectIdQueryHandler
{
    private readonly IWorkflowRepository _workflowRepository;

    public GetWorkflowsByProjectIdQueryHandler(IWorkflowRepository workflowRepository)
    {
        _workflowRepository = workflowRepository;
    }

    public async Task<Result<List<WorkflowDto>>> Handle(GetWorkflowsByProjectIdQuery query, CancellationToken cancellationToken)
    {
        var workflows = await _workflowRepository.GetByProjectIdAsync(query.ProjectId);

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