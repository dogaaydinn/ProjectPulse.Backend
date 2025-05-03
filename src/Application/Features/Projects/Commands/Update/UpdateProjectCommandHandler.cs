using Domain.Repositories;
using Shared.Results;

namespace Application.Features.Projects.Commands.Update;

public class UpdateProjectCommandHandler
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(command.Id);

        if (project is null)
            return Result.Failure(Error.NotFound("Project", command.Id));

        project.UpdateDetails(command.Name, command.Description, command.Status, command.Priority);
        project.SetSchedule(command.StartDate, command.EndDate);
        project.AssignManager(command.ManagerId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}