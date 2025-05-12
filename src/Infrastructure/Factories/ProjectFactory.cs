using Domain.Factories;
using Domain.Modules.Projects.Entities;
using Domain.Modules.Projects.Enums;
using Shared.Constants;
using Shared.Exceptions;

namespace Infrastructure.Factories;

public class ProjectFactory : IProjectFactory
{
    public Project Create(
        string name,
        string? description,
        DateTime startDate,
        DateTime? endDate,
        Guid managerId)
    {
        return Create(name, description, startDate, endDate, managerId, ProjectStatus.Planned, ProjectPriority.Medium);
    }

    public Project Create(
        string name,
        string? description,
        DateTime startDate,
        DateTime? endDate,
        Guid managerId,
        ProjectStatus projectStatus,
        ProjectPriority projectPriority)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new AppException(ErrorCodes.Validation, ValidationMessages.Project.ProjectNameRequired);

        if (managerId == Guid.Empty)
            throw new AppException(ErrorCodes.Validation, ValidationMessages.Project.ManagerIdRequired);

        if (endDate < startDate)
            throw new AppException(ErrorCodes.Validation, ValidationMessages.Common.StartDateMustBeBeforeEndDate);

        return new Project(name, description, startDate, endDate, managerId, projectStatus, projectPriority);
    }
}