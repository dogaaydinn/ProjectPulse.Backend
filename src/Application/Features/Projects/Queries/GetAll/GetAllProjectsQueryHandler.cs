using Application.DTOs;
using Shared.Results;

namespace Application.Features.Projects.Queries.GetAll;

public class GetAllProjectsQueryHandler
{
    private readonly IProjectRepository _projectRepository;

    public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<List<ProjectDto>>> Handle(GetAllProjectsQuery query, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetAllAsync();

        var dtoList = projects.Select(p => new ProjectDto(
            p.Id,
            p.Name,
            p.Description,
            p.StartDate,
            p.EndDate,
            p.ManagerId
        )).ToList();

        return Result<List<ProjectDto>>.Success(dtoList);
    }
}