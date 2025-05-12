using Domain.Core.Persistence;
using Domain.Modules.Projects.Repositories;
using Shared.Results;

namespace Application.Features.Projects.Commands.Delete;

public class DeleteProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
{
    public async Task<Result> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(command.Id);

        if (project is null)
            return Result.Failure(Error.NotFound("Project", command.Id));

        projectRepository.Delete(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}