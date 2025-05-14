using Application.Common.Validation;
using Application.DTOs.Project;
using Application.Features.Projects.Commands.Update;
using Application.Interfaces;
using Domain.Core.Persistence;
using Domain.Factories;
using Domain.Modules.Projects.Enums;
using Domain.Modules.Projects.Repositories;
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
        if (!ProjectStatus.TryFromName(request.Status, out var status))
            return Result<Guid>.Failure(Error.Validation("Invalid project status."));

        if (!ProjectPriority.TryFromName(request.Priority, out var priority))
            return Result<Guid>.Failure(Error.Validation("Invalid project priority."));

        var project = _projectFactory.Create(
            request.Name,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.ManagerId,
            status,
            priority);

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
            project.Priority.ToString()
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
            p.Priority.ToString()
        )).ToList();

        return Result<List<ProjectDto>>.Success(dtos);
    }

    #endregion

    #region UpdateProjectAsync
    public async Task<Result<ProjectDto>> UpdateProjectAsync(UpdateProjectRequest request)
    {
        var statusResult = request.Status.ConvertAsResult<ProjectStatus>();
        if (statusResult.IsFailure)
            return Result<ProjectDto>.Failure(statusResult.Error);

        var priorityResult = request.Priority.ConvertAsResult<ProjectPriority>();
        if (priorityResult.IsFailure)
            return Result<ProjectDto>.Failure(priorityResult.Error);

        var command = new UpdateProjectCommand(
            request.Id,
            request.Name,
            request.Description,
            request.Schedule,
            request.ManagerId,
            statusResult.Value,
            priorityResult.Value);

        var handler = new UpdateProjectCommandHandler(
            _projectRepository,
            _unitOfWork,
            new UpdateProjectCommandValidator());

        var result = await handler.Handle(command, CancellationToken.None);
        if (result.IsFailure)
            return Result<ProjectDto>.Failure(result.Error);

        var updated = await _projectRepository.GetByIdAsync(result.Value);
        if (updated is null)
            return Result<ProjectDto>.Failure(Error.NotFound("Project", request.Id));

        return Result<ProjectDto>.Success(new ProjectDto(
            updated.Id,
            updated.Name,
            updated.Description,
            updated.Schedule,
            updated.ManagerId,
            updated.Status.ToString(),
            updated.Priority.ToString()
        ));
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
