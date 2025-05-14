using Domain.Modules.Projects.Enums;

namespace Domain.Modules.Projects.Models;

using Shared.ValueObjects;

public sealed class CreateProjectModel
{
    public LocalizedString Name { get; }
    public LocalizedString Description { get; }
    public DateRange Schedule { get; }
    public Guid ManagerId { get; }
    public Guid CreatedByUserId { get; }
    public ProjectStatus Status { get; }
    public ProjectPriority Priority { get; }

    public CreateProjectModel(
        LocalizedString name,
        LocalizedString description,
        DateRange schedule,
        Guid managerId,
        Guid createdByUserId,
        ProjectStatus status,
        ProjectPriority priority)
    {
        Name = name;
        Description = description;
        Schedule = schedule;
        ManagerId = managerId;
        CreatedByUserId = createdByUserId;
        Status = status;
        Priority = priority;
    }
}