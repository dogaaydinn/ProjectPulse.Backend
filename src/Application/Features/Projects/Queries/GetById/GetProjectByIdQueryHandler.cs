using Application.DTOs;
using Application.DTOs.Project;
using Domain.Modules.Projects.Repositories;
using Shared.Results;

namespace Application.Features.Projects.Queries.GetById;

public class GetProjectByIdQueryHandler(IProjectRepository projectRepository)
{
    public async Task<Result<ProjectDto>> Handle(GetProjectByIdQuery query, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(query.ProjectId);

        if (project is null)
            return Result<ProjectDto>.Failure(Error.NotFound("Project", query.ProjectId));

        var dto = new ProjectDto(
            project.Id,
            project.Name,
            project.Description,
            project.Schedule,
            project.ManagerId,
            project.Status.ToString(),
            project.Priority.ToString()
        );

        return Result<ProjectDto>.Success(dto);
    }
}