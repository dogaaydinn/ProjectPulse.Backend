using Application.DTOs;
using Shared.Constants;
using Shared.Results;

namespace Application.Features.Tasks.Commands;

public class GetProjectByIdHandler
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectByIdHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<ProjectDto>> Handle(GetProjectByIdQuery query, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(query.ProjectId);
        if (project is null)
        {
            return Result<ProjectDto>.Failure(new Error(
                ErrorCodes.NotFound,
                ErrorMessages.ProjectNotFound));
        }

        var dto = new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status.ToString()
        };

        return Result<ProjectDto>.Success(dto);
    }
}