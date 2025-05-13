using Application.DTOs;
using Application.DTOs.Project;
using Domain.Modules.Projects.Repositories;
using Shared.Results;

namespace Application.Features.Projects.Queries.GetAll;

public class GetAllProjectsQueryHandler(IProjectRepository projectRepository)
{
    public async Task<Result<List<ProjectDto>>> Handle(GetAllProjectsQuery query, CancellationToken cancellationToken)
    {
        var projects = await projectRepository.GetAllAsync();

        var dtoList = projects.Select(p => new ProjectDto(
            p.Id,
            p.Name,
            p.Description,
            p.Schedule,
            p.ManagerId,
            p.Status.ToString(),
            p.Priority.ToString()
        )).ToList();

        return Result<List<ProjectDto>>.Success(dtoList);
    }
}