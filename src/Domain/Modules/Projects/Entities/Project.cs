using Domain.Modules.Projects.Enums;
using Domain.Modules.Tasks.Entities;
using Domain.Modules.Users.Entities;
using Shared.Base;
using Shared.Constants;
using Shared.Exceptions;
using Shared.Validation;
using Shared.ValueObjects;

namespace Domain.Modules.Projects.Entities;

public class Project : BaseAuditableEntity
{
    public LocalizedString Name { get; private set; } = null!;

    public LocalizedString? Description { get; private set; }
    public DateRange Schedule { get; private set; } = null!;

    public ProjectStatus Status { get; private set; } = ProjectStatus.Planned;
    public ProjectPriority Priority { get; private set; } = ProjectPriority.Medium;

    public Guid ManagerId { get; private set; }
    public User Manager { get; private set; } = null!;
    public Guid CreatedByUserId { get; private set; }

    public ICollection<TaskItem> Tasks { get; private set; } = new List<TaskItem>();
    public ICollection<Milestone> Milestones { get; private set; } = new List<Milestone>();
    public ICollection<Workflow> Workflows { get; private set; } = new List<Workflow>();
    public ICollection<TeamProject> TeamProjects { get; private set; } = new List<TeamProject>();
    public ICollection<Status> Statuses { get; private set; } = new List<Status>();

    protected Project() { }

    public Project(
        LocalizedString name,
        LocalizedString? description,
        DateRange schedule,
        Guid managerId,
        Guid createdByUserId,
        ProjectStatus status = default,
        ProjectPriority priority = default
    )
    {
        SetName(name);
        Description = description;
        Schedule = schedule;
        ManagerId = managerId;
        CreatedByUserId = createdByUserId;
        Status = status;
        Priority = priority;
    }

    public void UpdateDetails(LocalizedString name, LocalizedString? description, ProjectStatus status, ProjectPriority priority)
    {
        SetName(name);
        Description = description;
        Status = status;
        Priority = priority;
    }


    public void SetSchedule(DateRange schedule)
    {
        Schedule = schedule;
    }

    public void AssignManager(Guid managerId)
    {
        if (managerId == Guid.Empty)
            throw new AppException(ErrorCodes.Validation, ValidationMessages.Project.ManagerIdRequired);

        ManagerId = managerId;
    }

    private void SetName(LocalizedString name)
    {
        Guard.AgainstEmptyLocalized(name, ErrorCodes.Validation, ValidationMessages.Project.ProjectNameRequired);
        Name = name;
    }
}
