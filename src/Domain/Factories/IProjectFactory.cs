using Domain.Modules.Projects.Entities;
using Domain.Modules.Projects.Enums;
using Domain.Modules.Projects.Models;
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
        Guid createdByUserId,
        ProjectStatus status,
        ProjectPriority priority);

    Project Create(CreateProjectModel model); 
}