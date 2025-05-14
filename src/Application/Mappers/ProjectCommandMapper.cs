using Application.Common.Validation;
using Application.DTOs.Project;
using Application.Features.Projects.Commands.Create;
using Application.Features.Projects.Commands.Update;
using Domain.Modules.Projects.Enums;
using Shared.Results;
using Shared.ValueObjects;

namespace Application.Mappers;

public static class ProjectCommandMapper
{
    public static Result<CreateProjectCommand> ToCreateCommand(CreateProjectRequest dto)
    {
        var statusResult = dto.Status.ConvertAsResult<ProjectStatus>();
        if (statusResult.IsFailure)
            return Result<CreateProjectCommand>.Failure(statusResult.Error);

        var priorityResult = dto.Priority.ConvertAsResult<ProjectPriority>();
        if (priorityResult.IsFailure)
            return Result<CreateProjectCommand>.Failure(priorityResult.Error);

        return Result<CreateProjectCommand>.Success(new CreateProjectCommand(
            dto.Name.ToLocalizedString(),
            dto.Description?.ToLocalizedString(),
            dto.Schedule.ToDateRange(),
            dto.ManagerId,
            statusResult.Value,
            priorityResult.Value
        ));
    }

    public static Result<UpdateProjectCommand> ToUpdateCommand(UpdateProjectRequest dto)
    {
        var statusResult = dto.Status.ConvertAsResult<ProjectStatus>();
        if (statusResult.IsFailure)
            return Result<UpdateProjectCommand>.Failure(statusResult.Error);

        var priorityResult = dto.Priority.ConvertAsResult<ProjectPriority>();
        if (priorityResult.IsFailure)
            return Result<UpdateProjectCommand>.Failure(priorityResult.Error);

        return Result<UpdateProjectCommand>.Success(new UpdateProjectCommand(
            dto.Id,
            dto.Name.ToLocalizedString(),
            dto.Description?.ToLocalizedString(),
            dto.Schedule.ToDateRange(),
            dto.ManagerId,
            statusResult.Value,
            priorityResult.Value
        ));
    }
}