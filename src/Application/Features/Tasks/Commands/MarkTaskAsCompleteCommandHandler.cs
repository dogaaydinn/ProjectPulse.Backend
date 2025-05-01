using Application.Interfaces;
using Shared.Results;

namespace Application.Features.Tasks.Commands;

public class MarkTaskAsCompleteCommandHandler : IRequestHandler<MarkTaskAsCompleteCommand, Result>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationService _notificationService;

    public MarkTaskAsCompleteCommandHandler(
        ITaskRepository taskRepository,
        IUnitOfWork unitOfWork,
        INotificationService notificationService)
    {
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }

    public async Task<Result> Handle(MarkTaskAsCompleteCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.TaskId);
        if (task is null)
            return Result.Failure(Error.NotFound("Task"));
        task.MarkAsCompleted(/* Status.DoneId */ Guid.NewGuid());

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.NotifyTaskCompletedAsync(task.Id, task.AssigneeId ?? Guid.Empty);

        return Result.Success();
    }
}