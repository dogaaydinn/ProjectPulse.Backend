using Domain.Core.Persistence;
using Domain.Factories;
using Domain.Modules.Projects.Repositories;
using Shared.Results;

namespace Application.Features.Projects.Commands.Create;

public class CreateProjectCommandHandler(
    IProjectRepository projectRepository,
    IProjectFactory projectFactory,
    IUnitOfWork unitOfWork)
{
    public async Task<Result<Guid>> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = projectFactory.Create(
            command.Name,
            command.Description,
            command.StartDate,
            command.EndDate,
            command.ManagerId);

        await projectRepository.AddAsync(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(project.Id);
    }
}