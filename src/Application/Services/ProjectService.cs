using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using Domain.Factories;
using Domain.Repositories;
using Shared.Results;

namespace Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectFactory _projectFactory;
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProjectService(
        IProjectFactory projectFactory,
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork)
    {
        _projectFactory = projectFactory;
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    #region CreateProjectAsync

    public async Task<Result<Guid>> CreateProjectAsync(CreateProjectRequest request)
    {
        var project = _projectFactory.Create(
            request.Name,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.ManagerId);

        project.UpdateDetails(request.Name, request.Description, status, request.Priority);

        await _projectRepository.AddAsync(project);
        await _unitOfWork.SaveChangesAsync();

        return Result<Guid>.Success(project.Id);
    }


    #endregion

    #region GetProjectByIdAsync

    public async Task<Result<ProjectDto>> GetProjectByIdAsync(Guid projectId)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);

        if (project == null)
            return Result<ProjectDto>.Failure(Error.NotFound("Project", projectId));

        var dto = new ProjectDto(
            project.Id,
            project.Name,
            project.Description,
            project.StartDate,
            project.EndDate,
            project.ManagerId,
            project.Status.ToString(),
            project.ProjectPriority.ToString()
        );

        return Result<ProjectDto>.Success(dto);
    }

    #endregion

    #region GetAllProjectsAsync

    public async Task<Result<List<ProjectDto>>> GetAllProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync();

        var dtos = projects.Select(p => new ProjectDto(
            p.Id,
            p.Name,
            p.Description,
            p.StartDate,
            p.EndDate,
            p.ManagerId,
            p.Status.ToString(),
            p.ProjectPriority.ToString()
        )).ToList();

        return Result<List<ProjectDto>>.Success(dtos);
    }

    #endregion

    #region UpdateProjectAsync

    public async Task<Result<ProjectDto>> UpdateProjectAsync(UpdateProjectRequest request)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);

        if (project == null)
            return Result<ProjectDto>.Failure(Error.NotFound("Project", request.Id));

        if (!Enum.TryParse<ProjectStatus>(request.Status, true, out var status))
            return Result<ProjectDto>.Failure(Error.Validation("Invalid project status."));

        if (!Enum.TryParse<ProjectPriority>(request.Priority, true, out var priority))
            return Result<ProjectDto>.Failure(Error.Validation("Invalid project priority."));

        project.UpdateDetails(request.Name, request.Description, status, priority);
        await _unitOfWork.SaveChangesAsync();

        var dto = new ProjectDto(
            project.Id,
            project.Name,
            project.Description,
            project.StartDate,
            project.EndDate,
            project.ManagerId,
            project.Status.ToString(),
            project.ProjectPriority.ToString()
        );

        return Result<ProjectDto>.Success(dto);
    }

    #endregion

    #region DeleteProjectAsync

    public async Task<Result> DeleteProjectAsync(Guid projectId)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);

        if (project == null)
            return Result.Failure(Error.NotFound("Project", projectId));

        _projectRepository.Delete(project);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    #endregion
}
