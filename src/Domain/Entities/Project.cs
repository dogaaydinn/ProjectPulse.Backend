using Domain.Enums;
using Shared.Base;
using Shared.Exceptions;

namespace Domain.Entities;

public class Project : BaseAuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    public ProjectStatus Status { get; private set; } = ProjectStatus.Planned;
    public Priority Priority { get; private set; } = Priority.Normal;

    public Guid ManagerId { get; private set; }
    public User Manager { get; private set; } = null!;

    public ICollection<TaskItem> Tasks { get; private set; } = new List<TaskItem>();
    public ICollection<Milestone> Milestones { get; private set; } = new List<Milestone>();
    public ICollection<Workflow> Workflows { get; private set; } = new List<Workflow>();
    public ICollection<TeamProject> TeamProjects { get; private set; } = new List<TeamProject>();
    public ICollection<Status> Statuses { get; private set; } = new List<Status>();

    protected Project() { }

    public Project(string name, string? description, DateTime startDate, DateTime? endDate, Guid managerId)
    {
        SetName(name);
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        ManagerId = managerId;
    }

    public void UpdateDetails(string name, string? description, ProjectStatus status, Priority priority)
    {
        SetName(name);
        Description = description;
        Status = status;
        Priority = priority;
    }

    public void SetSchedule(DateTime start, DateTime? end)
    {
        if (end.HasValue && end < start)
            throw new AppException("Validation.Projects.Schedule", "End date cannot be earlier than start date.");

        StartDate = start;
        EndDate = end;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new AppException("Validation.Projects.Name", "Projects name is required.");
        Name = name;
    }

    public void AssignManager(Guid managerId)
    {
        if (managerId == Guid.Empty)
            throw new AppException("Validation.Projects.Manager", "Manager ID cannot be empty.");
        ManagerId = managerId;
    }
}
