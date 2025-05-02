using Application.DTOs;
using Shared.Results;

namespace Application.Features.Projects.Queries.GetAll;

public class GetAllQueryHandler
{
    private readonly IProjectRepository _projectRepository;

    public GetAllQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<List<ProjectDto>>> Handle(GetAllQuery query, CancellationToken cancellationToken)
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