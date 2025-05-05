using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using Domain.Factories;
using Domain.Repositories;
using Shared.Results;

namespace Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskFactory _taskFactory;
    private readonly ITaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TaskService(
        ITaskFactory taskFactory,
        ITaskRepository taskRepository,
        IUnitOfWork unitOfWork)
    {
        _taskFactory = taskFactory;
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> CreateTaskAsync(CreateTaskRequest request)
    {
        var task = _taskFactory.Create(
            request.Title,
            request.Description,
            Enum.Parse<TaskPriority>(request.Priority, true),
            Enum.Parse<TaskType>(request.Type, true),
            request.ProjectId,
            request.AssigneeId,
            request.ReporterId
        );

        task.SetSchedule(request.StartDate, request.EndDate);

        await _taskRepository.AddAsync(task);
        await _unitOfWork.SaveChangesAsync();

        return Result<Guid>.Success(task.Id);
    }

    public async Task<Result<TaskDto>> GetTaskByIdAsync(Guid id)
    {
        var task = await _taskRepository.GetByIdAsync(id);

        if (task is null)
            return Result<TaskDto>.Failure(Error.NotFound("Task", id));

        var dto = new TaskDto(
            task.Id,
            task.Title,
            task.Description,
            task.Priority.ToString(),
            task.Type.ToString(),
            task.ProjectId,
            task.AssigneeId,
            task.ReporterId,
            task.Schedule?.Start,
            task.Schedule?.End
        );

        return Result<TaskDto>.Success(dto);
    }

    public async Task<Result<List<TaskDto>>> GetAllTasksAsync()
    {
        var tasks = await _taskRepository.GetAllAsync();

        var dtos = tasks.Select(t => new TaskDto(
            t.Id,
            t.Title,
            t.Description,
            t.Priority.ToString(),
            t.Type.ToString(),
            t.ProjectId,
            t.AssigneeId,
            t.ReporterId,
            t.Schedule?.Start,
            t.Schedule?.End
        )).ToList();

        return Result<List<TaskDto>>.Success(dtos);
    }

    public async Task<Result<TaskDto>> UpdateTaskAsync(UpdateTaskRequest request)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);

        if (task is null)
            return Result<TaskDto>.Failure(Error.NotFound("Task", request.Id));

        task.SetTitle(request.Title);
        task.SetSchedule(request.StartDate, request.EndDate);
        task.AssignTo(request.AssigneeId);
        task.SetReporter(request.ReporterId);
        
        if (Enum.TryParse<TaskPriority>(request.Priority, true, out var priority))
            task.ChangePriority(priority);

        if (Enum.TryParse<TaskType>(request.Type, true, out var type))
            task.ChangeType(type);

        await _unitOfWork.SaveChangesAsync();

        var dto = new TaskDto(
            task.Id,
            task.Title,
            task.Description,
            task.Priority.ToString(),
            task.Type.ToString(),
            task.ProjectId,
            task.AssigneeId,
            task.ReporterId,
            task.Schedule?.Start,
            task.Schedule?.End
        );

        return Result<TaskDto>.Success(dto);
    }

    public async Task<Result> DeleteTaskAsync(Guid taskId)
    {
        var task = await _taskRepository.GetByIdAsync(taskId);

        if (task is null)
            return Result.Failure(Error.NotFound("Task", taskId));

        _taskRepository.Delete(task);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
