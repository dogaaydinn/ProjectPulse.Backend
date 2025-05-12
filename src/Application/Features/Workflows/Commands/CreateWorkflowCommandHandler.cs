using Domain.Core.Persistence;
using Domain.Factories;
using Domain.Modules.Projects.Repositories;
using Shared.Results;

namespace Application.Features.Workflows.Commands;

public class CreateWorkflowCommandHandler(
    IWorkflowFactory workflowFactory,
    IWorkflowRepository workflowRepository,
    IUnitOfWork unitOfWork)
{
    public async Task<Result<Guid>> Handle(CreateWorkflowCommand command, CancellationToken cancellationToken)
    {
        var workflow = workflowFactory.Create(
            command.Name,
            command.ProjectId,
            command.IsDefault);

        await workflowRepository.AddAsync(workflow);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(workflow.Id);
    }
}