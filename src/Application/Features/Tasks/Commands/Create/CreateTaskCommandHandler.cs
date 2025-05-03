using Domain.Factories;
using Domain.Repositories;
using Shared.Results;

namespace Application.Features.Tasks.Commands.Create;

public class CreateTaskCommandHandler(
    ITaskFactory taskFactory,
    ITaskRepository taskRepository,
    IUnitOfWork unitOfWork)
{
    public async Task<Result<Guid>> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        var task = taskFactory.Create(
            command.Title,
            command.Description,
            command.Priority,
            command.Type,
            command.ProjectId,
            command.AssigneeId,
            command.ReporterId
        );

        task.SetSchedule(command.StartDate, command.EndDate);

        await taskRepository.AddAsync(task);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(task.Id);
    }
}