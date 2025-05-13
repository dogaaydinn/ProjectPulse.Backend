using Application.DTOs;
using Application.DTOs.Task;
using Shared.Results;

namespace Application.Interfaces;

public interface ITaskService
{
    Task<Result<Guid>> CreateTaskAsync(CreateTaskRequest request);
    Task<Result<TaskDto>> GetTaskByIdAsync(Guid taskId);
    Task<Result<List<TaskDto>>> GetAllTasksAsync();
    Task<Result<TaskDto>> UpdateTaskAsync(UpdateTaskRequest request);
    Task<Result> DeleteTaskAsync(Guid taskId);
}