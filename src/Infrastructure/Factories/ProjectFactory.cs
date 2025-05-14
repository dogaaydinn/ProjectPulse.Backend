using Domain.Factories;
using Domain.Modules.Projects.Entities;
using Domain.Modules.Projects.Enums;
using Shared.Constants;
using Shared.Validation;
using Shared.ValueObjects;

namespace Infrastructure.Factories;

public class ProjectFactory : IProjectFactory
{
    public Project Create(
        LocalizedString name,
        LocalizedString description,
        DateRange schedule,
        Guid managerId)
    {
        return Create(name, description, schedule, managerId, Guid.NewGuid(), ProjectStatus.Planned, ProjectPriority.Medium);
    }

    public Project Create(
        LocalizedString name,
        LocalizedString description,
        DateRange schedule,
        Guid managerId,
        Guid createdByUserId,
        ProjectStatus status,
        ProjectPriority priority)
    {
        Guard.AgainstEmptyLocalized(name, ErrorCodes.Validation, ValidationMessages.Project.ProjectNameRequired);
        Guard.AgainstEmptyDateRange(schedule, ErrorCodes.Validation, ValidationMessages.Project.ScheduleRequired);
        Guard.AgainstDefaultGuid(managerId, ErrorCodes.Validation, ValidationMessages.Project.ManagerIdRequired);

        return new Project(name, description, schedule, managerId, createdByUserId, status, priority);
    }
}