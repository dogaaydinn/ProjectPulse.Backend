using Domain.Modules.Projects.Entities;
using Domain.Modules.Projects.Enums;
using Shared.ValueObjects;

namespace Domain.Factories;

public interface IProjectFactory
{
    Project Create(
        LocalizedString name,
        LocalizedString? description,
        DateRange schedule,
        Guid managerId);

    Project Create(
        LocalizedString name,
        LocalizedString? description,
        DateRange schedule,
        Guid managerId,
        ProjectStatus status,
        ProjectPriority priority);
}