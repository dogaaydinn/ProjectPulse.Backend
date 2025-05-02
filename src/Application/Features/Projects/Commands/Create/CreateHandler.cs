using Domain.Factories;
using Shared.Results;

namespace Application.Features.Projects.Commands.Create;

public class CreateCommandHandler
{
    private readonly ITaskFactory _taskFactory;
    private readonly ITaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCommandHandler(
        ITaskFactory taskFactory,
        ITaskRepository taskRepository,
        IUnitOfWork unitOfWork)
    {
        _taskFactory = taskFactory;
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateCommand command, CancellationToken cancellationToken)
    {
        var task = _taskFactory.Create(
            command.Title,
            command.Description,
            command.Priority,
            command.Type,
            command.ProjectId,
            command.AssigneeId,
            command.ReporterId
        );

        task.SetSchedule(command.StartDate, command.EndDate);

        await _taskRepository.AddAsync(task);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(task.Id);
    }
}