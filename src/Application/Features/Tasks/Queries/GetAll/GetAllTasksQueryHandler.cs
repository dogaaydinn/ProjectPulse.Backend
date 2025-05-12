using Application.DTOs;
using Domain.Modules.Tasks.Repositories;
using Shared.Results;

namespace Application.Features.Tasks.Queries.GetAll;

public class GetAllTasksQueryHandler(ITaskRepository taskRepository)
{
    public async Task<Result<List<TaskDto>>> Handle(GetAllTasksQuery query, CancellationToken cancellationToken)
    {
        var tasks = await taskRepository.GetAllAsync();

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