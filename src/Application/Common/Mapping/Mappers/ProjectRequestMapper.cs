using Application.DTOs.Project.Interfaces;
using Shared.Results;
using Shared.ValueObjects;
using Domain.Modules.Projects.Enums;
using Application.Common.Validation;
using Application.DTOs.Common;

namespace Application.Common.Mapping.Mappers;

public static class ProjectRequestMapper
{
    public static Result<TCommand> MapRequest<TCommand>(
        IProjectUpdateRequest request,
        Func<Guid, LocalizedString, LocalizedString?, DateRange, Guid, ProjectStatus, ProjectPriority, TCommand> factory)
    {
        var statusResult = request.Status.ConvertAsResult<ProjectStatus>();
        if (statusResult.IsFailure)
            return Result<TCommand>.Failure(statusResult.Error);

        var priorityResult = request.Priority.ConvertAsResult<ProjectPriority>();
        if (priorityResult.IsFailure)
            return Result<TCommand>.Failure(priorityResult.Error);

        return Result<TCommand>.Success(factory(
            request.Id,
            request.Name.ToLocalizedString(),
            request.Description?.ToLocalizedString(),
            request.Schedule.ToDateRange(),
            request.ManagerId,
            statusResult.Value,
            priorityResult.Value
        ));
    }

    public static Result<TCommand> MapRequest<TCommand>(
        IProjectCreateRequest request,
        Func<LocalizedString, LocalizedString?, DateRange, Guid, ProjectStatus, ProjectPriority, TCommand> factory)
    {
        var statusResult = request.Status.ConvertAsResult<ProjectStatus>();
        if (statusResult.IsFailure)
            return Result<TCommand>.Failure(statusResult.Error);

        var priorityResult = request.Priority.ConvertAsResult<ProjectPriority>();
        if (priorityResult.IsFailure)
            return Result<TCommand>.Failure(priorityResult.Error);

        return Result<TCommand>.Success(factory(
            request.Name.ToLocalizedString(),
            request.Description?.ToLocalizedString(),
            request.Schedule.ToDateRange(),
            request.ManagerId,
            statusResult.Value,
            priorityResult.Value
        ));
    }
}