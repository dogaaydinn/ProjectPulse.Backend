using Domain.Factories;
using Domain.Modules.Projects.Entities;
using Domain.Modules.Projects.Enums;
using Domain.Modules.Projects.Models;
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

    public Project Create(CreateProjectModel model)
    {
        Guard.AgainstNull(model, ProjectErrors.ModelRequired);
        Guard.AgainstEmptyLocalized(model.Name, ProjectErrors.NameRequired);
        Guard.AgainstEmptyDateRange(model.Schedule, ProjectErrors.ScheduleRequired);
        Guard.AgainstDefaultGuid(model.ManagerId, ProjectErrors.ManagerIdRequired);
        Guard.AgainstDefaultGuid(model.CreatedByUserId, ProjectErrors.CreatedByRequired);

        return new Project(
            model.Name,
            model.Description,
            model.Schedule,
            model.ManagerId,
            model.CreatedByUserId,
            model.Status,
            model.Priority);
    }
}