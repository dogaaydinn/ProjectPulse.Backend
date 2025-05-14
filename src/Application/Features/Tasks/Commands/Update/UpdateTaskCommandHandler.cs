using Application.DTOs;
using Application.DTOs.Task;
using Domain.Core.Persistence;
using Domain.Modules.Tasks.Enums;
using Domain.Modules.Tasks.Repositories;
using Shared.Results;

namespace Application.Features.Tasks.Commands.Update;

public class UpdateTaskCommandHandler(ITaskRepository taskRepository, IUnitOfWork unitOfWork)
{
    public async Task<Result> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(command.Id);

        if (task is null)
            return Result.Failure(Error.NotFound("Task", command.Id));

        var priority = TaskPriority.ConvertOrThrow(command.Priority);
        var type = TaskType.ConvertOrThrow(command.Type);

        task.SetTitle(command.Title);
        task.SetSchedule(command.StartDate, command.EndDate);
        task.AssignTo(command.AssigneeId);
        task.SetReporter(command.ReporterId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}