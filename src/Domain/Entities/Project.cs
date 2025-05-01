using Shared.Base;

namespace Domain.Entities;

public class Project : BaseAuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public string Status { get; private set; } = "Planned"; // Opsiyonel enumlaştırılır
    public string Priority { get; private set; } = "Normal"; // Opsiyonel enumlaştırılır

    // Foreign Key
    public Guid ManagerId { get; private set; }

    // Navigation Property
    public User Manager { get; private set; } = null!;

    public ICollection<TaskItem> Tasks { get; private set; } = new List<TaskItem>();
    public ICollection<Milestone> Milestones { get; private set; } = new List<Milestone>();
    public ICollection<Workflow> Workflows { get; private set; } = new List<Workflow>();
    public ICollection<TeamProject> TeamProjects { get; private set; } = new List<TeamProject>();
    public ICollection<Status> Statuses { get; private set; } = new List<Status>();

    private Project() { }

    public Project(string name, string? description, DateTime startDate, DateTime? endDate, Guid managerId)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        ManagerId = managerId;
    }

    public void UpdateDetails(string name, string? description, string status, string priority)
    {
        Name = name;
        Description = description;
        Status = status;
        Priority = priority;
    }
}