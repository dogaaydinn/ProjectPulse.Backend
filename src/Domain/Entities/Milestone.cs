using Shared.Base;
using Shared.Exceptions;

namespace Domain.Entities;

public class Milestone : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime DueDate { get; private set; }
    public bool IsCompleted { get; private set; }

    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; } = null!;

    protected Milestone() { }

    internal Milestone(string name, DateTime dueDate, Guid projectId, string? description = null)
    {
        SetName(name);
        SetDueDate(dueDate);

        ProjectId = projectId;
        Description = description;
        IsCompleted = false;
    }

    public void MarkComplete()
    {
        if (IsCompleted)
            throw new AppException("Milestone.AlreadyCompleted", "Milestone is already marked as completed.");

        IsCompleted = true;
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