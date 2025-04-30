using Application.DTOs;
using Application.Interfaces;
using Shared.Results;

namespace Application.Services;

public class ProjectService : IProjectService
{
    private static readonly List<ProjectDto> _inMemoryProjects = [];

    public async Task<Result<ProjectDto>> CreateProjectAsync(CreateProjectRequest request)
    {
        var project = new ProjectDto
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Deadline = request.Deadline
        };

        _inMemoryProjects.Add(project);
        return Result<ProjectDto>.Success(project);
    }

    public async Task<Result<List<ProjectDto>>> GetAllProjectsAsync()
    {
        return Result<List<ProjectDto>>.Success(_inMemoryProjects);
    }
}