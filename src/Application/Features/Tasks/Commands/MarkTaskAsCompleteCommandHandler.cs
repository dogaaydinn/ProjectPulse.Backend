using Application.Interfaces;
using Shared.Results;

namespace Application.Features.Tasks.Commands;

public class MarkTaskAsCompleteHandler
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationService _notificationService;

    public MarkTaskAsCompleteHandler(
        ITaskRepository taskRepository,
        IUnitOfWork unitOfWork,
        INotificationService notificationService)
    {
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }

    public async Task<Result> Handle(MarkTaskAsCompleteCommand command, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(command.TaskId);
        if (task is null)
            return Result.Failure(Error.NotFound("Task"));

        task.MarkAsCompleted(/* Status.DoneId */ Guid.NewGuid());

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _notificationService.NotifyTaskCompletedAsync(task.Id, task.AssigneeId ?? Guid.Empty);

        return Result.Success();
    }
}