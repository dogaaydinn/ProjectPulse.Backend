using Application.DTOs;
using Shared.Results;

namespace Application.Interfaces;

public interface IProjectService
{
    Task<Result<ProjectDto>> CreateProjectAsync(CreateProjectRequest request);
    Task<Result<List<ProjectDto>>> GetAllProjectsAsync();
}