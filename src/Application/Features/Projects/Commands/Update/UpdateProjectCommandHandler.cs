using Domain.Core.Persistence;
using Domain.Modules.Projects.Repositories;
using Shared.Results;

namespace Application.Features.Projects.Commands.Update;

public class UpdateProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
{
    public async Task<Result> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(command.Id);

        if (project is null)
            return Result.Failure(Error.NotFound("Project", command.Id));

        project.UpdateDetails(command.Name, command.Description, command.Status, command.Priority);
        project.SetSchedule(command.StartDate, command.EndDate);
        project.AssignManager(command.ManagerId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}