using Domain.Modules.Projects.Entities;
using Domain.Modules.Projects.Enums;

namespace Domain.Factories;

public interface IProjectFactory
{
    Project Create(
        string name,
        string? description,
        DateTime startDate,
        DateTime? endDate,
        Guid managerId);

    Project Create(
        string name,
        string? description,
        DateTime startDate,
        DateTime? endDate,
        Guid managerId,
        ProjectStatus status,
        ProjectPriority priority);
}