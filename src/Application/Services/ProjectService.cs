using Application.DTOs;
using Application.Interfaces;
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

    public async Task<Result<Guid>> CreateProjectAsync(CreateProjectRequest request)
    {
        var project = _projectFactory.Create(
            request.Name,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.ManagerId);

        await _projectRepository.AddAsync(project);
        await _unitOfWork.SaveChangesAsync();

        return Result<Guid>.Success(project.Id);
    }

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
            project.ManagerId);

        return Result<ProjectDto>.Success(dto);
    }

    public async Task<Result<List<ProjectDto>>> GetAllProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync();

        var dtos = projects.Select(p => new ProjectDto(
            p.Id,
            p.Name,
            p.Description,
            p.StartDate,
            p.EndDate,
            p.ManagerId)).ToList();

        return Result<List<ProjectDto>>.Success(dtos);
    }

    public async Task<Result<ProjectDto>> UpdateProjectAsync(UpdateProjectRequest request)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);

        if (project == null)
            return Result<ProjectDto>.Failure(Error.NotFound("Project", request.Id));

        project.UpdateDetails(request.Name, request.Description, request.Status, request.Priority);
        await _unitOfWork.SaveChangesAsync();

        var dto = new ProjectDto(
            project.Id,
            project.Name,
            project.Description,
            project.StartDate,
            project.EndDate,
            project.ManagerId);

        return Result<ProjectDto>.Success(dto);
    }

    public async Task<Result> DeleteProjectAsync(Guid projectId)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);

        if (project == null)
            return Result.Failure(Error.NotFound("Project", projectId));

        _projectRepository.Delete(project);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
