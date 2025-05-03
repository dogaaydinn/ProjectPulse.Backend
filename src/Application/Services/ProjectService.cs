using Application.DTOs;
using Application.Interfaces;
using Shared.Results;

namespace Application.Services;

public class ProjectService : IProjectService
{
    private static readonly List<ProjectDto> InMemoryProjects = [];

    public Task<Result<ProjectDto>> CreateProjectAsync(CreateProjectRequest request)
    {
        var project = new ProjectDto(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.ManagerId
        );

        InMemoryProjects.Add(project);
        return Task.FromResult(Result<ProjectDto>.Success(project));
    }

    public async Task<Result<List<ProjectDto>>> GetAllProjectsAsync()
    {
        await Task.CompletedTask;
        return Result<List<ProjectDto>>.Success(InMemoryProjects);
    }
}