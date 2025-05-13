using Application.Common.Handlers;
using Application.Common.Validation;
using Domain.Core.Persistence;
using Domain.Modules.Projects.Repositories;
using Shared.Results;
using Shared.ValueObjects;

namespace Application.Features.Projects.Commands.Update;

public class UpdateProjectCommandHandler 
    : BaseCommandHandler<UpdateProjectCommand, Result>
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

    public async Task<Result> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        return await ValidateAndExecuteAsync(command, async () =>
        {
            var project = await _projectRepository.GetByIdAsync(command.Id);
            if (project is null)
                return Result.Failure(Error.NotFound(nameof(project), command.Id));

            var schedule = new DateRange(command.Schedule.Start, command.Schedule.End);
            project.UpdateDetails(command.Name, command.Description, command.Status, command.Priority);
            project.SetSchedule(schedule);
            project.AssignManager(command.ManagerId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }

}