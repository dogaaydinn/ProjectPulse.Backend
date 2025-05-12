using Application.DTOs;
using Domain.Modules.Tasks.Repositories;
using Shared.Results;

namespace Application.Features.Tasks.Queries.GetById;

public class GetTaskByIdQueryHandler(ITaskRepository taskRepository)
{
    public async Task<Result<TaskDto>> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(query.Id);

        if (task is null)
            return Result<TaskDto>.Failure(Error.NotFound("Task", query.Id));

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
}