using Application.DTOs;
using Application.DTOs.Workflow;
using Shared.Results;

namespace Application.Interfaces;

public interface IWorkflowService
{
    Task<Result<Guid>> CreateWorkflowAsync(CreateWorkflowRequest request);
    Task<Result<WorkflowDto>> GetWorkflowByIdAsync(Guid workflowId);
    Task<Result<List<WorkflowDto>>> GetWorkflowsByProjectIdAsync(Guid projectId);
    Task<Result> DeleteWorkflowAsync(Guid workflowId);
}