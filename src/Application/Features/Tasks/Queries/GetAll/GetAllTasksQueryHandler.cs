using Application.DTOs;
using Domain.Repositories;
using Shared.Results;

namespace Application.Features.Tasks.Queries.GetAll;

public class GetAllTasksQueryHandler
{
    private readonly ITaskRepository _taskRepository;

    public GetAllTasksQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result<List<TaskDto>>> Handle(GetAllTasksQuery query, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllAsync();

        var dtoList = tasks.Select(task => new TaskDto(
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
        )).ToList();

        return Result<List<TaskDto>>.Success(dtoList);
    }
}