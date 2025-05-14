using Application.Common.Handlers;
using Application.Common.Validation;
using Domain.Core.Persistence;
using Domain.Modules.Projects.Repositories;
using Shared.Results;

namespace Application.Features.Projects.Commands.Update;

public class UpdateProjectCommandHandler 
    : BaseCommandHandler<UpdateProjectCommand, Guid>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProjectCommandHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork,
        IValidator<UpdateProjectCommand> validator)
        : base(validator)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        return await ValidateAndExecuteAsync(command, async () =>
        {
            var project = await _projectRepository.GetByIdAsync(command.Id);
            if (project is null)
                return Result<Guid>.Failure(Error.NotFound("Project", command.Id));

            project.UpdateDetails(
                command.Name,
                command.Description,
                command.Status,
                command.Priority);

            project.SetSchedule(command.Schedule);
            project.AssignManager(command.ManagerId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(project.Id);
        });
    }
}