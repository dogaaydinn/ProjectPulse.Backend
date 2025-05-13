using Application.DTOs;
using Application.DTOs.Project;
using Shared.Results;

namespace Application.Interfaces;

public interface IProjectService
{
    Task<Result<Guid>> CreateProjectAsync(CreateProjectRequest request);
    Task<Result<ProjectDto>> GetProjectByIdAsync(Guid projectId);
    Task<Result<List<ProjectDto>>> GetAllProjectsAsync();
    Task<Result<ProjectDto>> UpdateProjectAsync(UpdateProjectRequest request);
    Task<Result> DeleteProjectAsync(Guid projectId);
}