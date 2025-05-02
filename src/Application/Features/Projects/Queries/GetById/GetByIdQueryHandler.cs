using Application.DTOs;
using Shared.Results;

namespace Application.Features.Projects.Queries.GetById;

public class GetByIdQueryHandler
{
    private readonly IProjectRepository _projectRepository;

    public GetByIdQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<ProjectDto>> Handle(GetProjectByIdQuery query, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(query.Id);

        if (project is null)
            return Result<ProjectDto>.Failure(Error.NotFound("Project", query.Id));

        var dto = new ProjectDto(
            project.Id,
            project.Name,
            project.Description,
            project.StartDate,
            project.EndDate,
            project.ManagerId
        );

        return Result<ProjectDto>.Success(dto);
    }
}