using Application.DTOs;
using Application.Interfaces;
using Domain.Factories;
using Domain.Repositories;
using Shared.Results;

namespace Application.Services;

public class ProjectService(
    IProjectFactory projectFactory,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork)
    : IProjectService
{
    #region CreateProjectAsync

    public async Task<Result<Guid>> CreateProjectAsync(CreateProjectRequest request)
    {
        var project = projectFactory.Create(
            request.Name,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.ManagerId);

        await projectRepository.AddAsync(project);
        await unitOfWork.SaveChangesAsync();

        return Result<Guid>.Success(project.Id);
    }

    #endregion
    
    #region GetProjectByIdAsync

    public async Task<Result<ProjectDto>> GetProjectByIdAsync(Guid projectId)
    {
        var project = await projectRepository.GetByIdAsync(projectId);

        if (project == null)
            return Result<ProjectDto>.Failure(Error.NotFound("Project", projectId));

        var dto = new ProjectDto(
            project.Id,
            project.Name,
            project.Description,
            project.StartDate,
            project.EndDate,
            project.ManagerId);

        return Result<ProjectDto>.Success(dto);
    }


    #endregion

    #region GetAllProjectsAsync

    public async Task<Result<List<ProjectDto>>> GetAllProjectsAsync()
    {
        var projects = await projectRepository.GetAllAsync();

        var dtos = projects.Select(p => new ProjectDto(
            p.Id,
            p.Name,
            p.Description,
            p.StartDate,
            p.EndDate,
            p.ManagerId)).ToList();

        return Result<List<ProjectDto>>.Success(dtos);
    }

    #endregion
    
    #region UpdateProjectAsync

    public async Task<Result<ProjectDto>> UpdateProjectAsync(UpdateProjectRequest request)
    {
        var project = await projectRepository.GetByIdAsync(request.Id);

        if (project == null)
            return Result<ProjectDto>.Failure(Error.NotFound("Project", request.Id));

        project.UpdateDetails(request.Name, request.Description, request.Status, request.Priority);
        await unitOfWork.SaveChangesAsync();

        var dto = new ProjectDto(
            project.Id,
            project.Name,
            project.Description,
            project.StartDate,
            project.EndDate,
            project.ManagerId);

        return Result<ProjectDto>.Success(dto);
    }

    #endregion
    
    #region DeleteProjectAsync

    public async Task<Result> DeleteProjectAsync(Guid projectId)
    {
        var project = await projectRepository.GetByIdAsync(projectId);

        if (project == null)
            return Result.Failure(Error.NotFound("Project", projectId));

        projectRepository.Delete(project);
        await unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    #endregion

}
