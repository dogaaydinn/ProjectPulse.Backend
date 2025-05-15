using Domain.Modules.Projects.Enums;
using Domain.Modules.Tasks.Entities;
using Domain.Modules.Users.Entities;
using Shared.Base;
using Shared.Exceptions;
using Shared.Results.Errors;
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


        Ensure.NotDefault(managerId, ProjectErrors.ManagerIdRequired);
        Ensure.NotDefault(createdByUserId, ProjectErrors.CreatedByRequired);

        SetName(name);
        SetDescription(description);
        SetSchedule(schedule);
        AssignManager(managerId);

        CreatedByUserId = Guard.AgainstDefaultGuidReturn(createdByUserId, ProjectErrors.CreatedByRequired);

        Status = status;
        Priority = priority;
    }


    public void Update(
        LocalizedString name,
        LocalizedString? description,
        DateRange schedule,
        Guid managerId,
        ProjectStatus status,
        ProjectPriority priority)
    {
        Ensure.NotEmpty(name, ProjectErrors.NameRequired);
        Ensure.NotEmpty(schedule, ProjectErrors.ScheduleRequired);
        Ensure.NotDefault(managerId, ProjectErrors.ManagerIdRequired);

        Name = name;
        Description = description;
        Schedule = schedule;
        ManagerId = managerId;
        Status = status;
        Priority = priority;
    }

    private void SetName(LocalizedString name)
    {
        Ensure.NotEmptyLocalized(name, ProjectErrors.NameRequired);
        Name = name;
    }

    private void SetDescription(LocalizedString? description)
    {
        if (description is not null && description.IsEmpty())
            throw new AppException(ProjectErrors.DescriptionTooLong(1000)); 
        Description = description;
    }

    private void SetSchedule(DateRange schedule)
    {
        Ensure.NotEmptyDateRange(schedule, ProjectErrors.ScheduleRequired);
        Schedule = schedule;
    }

    private void AssignManager(Guid managerId)
    {
        ManagerId = EnsureNotDefaultGuid(managerId, ProjectErrors.ManagerIdRequired);
    }
}
