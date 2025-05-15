using Domain.Factories;
using Domain.Modules.Projects.Entities;
using Domain.Modules.Projects.Enums;
using Shared.ValueObjects;
using Shared.Validation;
using Shared.Results.Errors; 

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
        Guard.AgainstEmptyLocalized(name, ProjectErrors.NameRequired);
        Guard.AgainstEmptyDateRange(schedule, ProjectErrors.ScheduleRequired);
        Guard.AgainstDefaultGuid(managerId, ProjectErrors.ManagerIdRequired);
        Guard.AgainstDefaultGuid(createdByUserId, ProjectErrors.CreatedByRequired);

        return new Project(name, description, schedule, managerId, createdByUserId, status, priority);
    }
}