using Domain.Factories;
using Domain.Repositories;
using Shared.Results;

namespace Application.Features.Workflows.Commands;

public class CreateWorkflowCommandHandler
{
    private readonly IWorkflowFactory _workflowFactory;
    private readonly IWorkflowRepository _workflowRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWorkflowCommandHandler(
        IWorkflowFactory workflowFactory,
        IWorkflowRepository workflowRepository,
        IUnitOfWork unitOfWork)
    {
        _workflowFactory = workflowFactory;
        _workflowRepository = workflowRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateWorkflowCommand command, CancellationToken cancellationToken)
    {
        var workflow = _workflowFactory.Create(
            command.Name,
            command.ProjectId,
            command.IsDefault);

        await _workflowRepository.AddAsync(workflow);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(workflow.Id);
    }
}