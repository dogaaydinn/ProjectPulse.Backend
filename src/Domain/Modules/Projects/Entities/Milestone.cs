using Domain.Modules.Projects.Enums;
using Domain.Modules.Projects.ValueObjects;
using Shared.Base;
using Shared.Exceptions;

namespace Domain.Modules.Projects.Entities;

public class Milestone : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public MilestoneSchedule Schedule { get; private set; } = null!;

    public bool IsCompleted { get; private set; }

    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; } = null!;
    public MilestoneStatus Status { get; private set; } = MilestoneStatus.Planned;
    
    protected Milestone() { }

    internal Milestone(string name, MilestoneSchedule schedule, Guid projectId, string? description = null)
    {
        SetName(name);
        Schedule = schedule;
        ProjectId = projectId;
        Description = description;
        Status = MilestoneStatus.Planned;
    }

    public void MarkComplete()
    {
        if (Status == MilestoneStatus.Completed)
            throw new AppException("Milestone.AlreadyCompleted", "Milestone is already marked as completed.");

        Status = MilestoneStatus.Completed;
    }
    
    public void Update(string name, string? description, DateTime dueDate)
    {
        SetName(name);
        SetDueDate(dueDate);
        Description = description;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new AppException("Validation.Milestone.Name", "Milestone name cannot be empty.");
        Name = name;
    }

    private void SetDueDate(DateTime dueDate)
    {
        if (dueDate < DateTime.UtcNow)
            throw new AppException("Validation.Milestone.DueDate", "Due date must be in the future.");
        DueDate = dueDate;
    }
}