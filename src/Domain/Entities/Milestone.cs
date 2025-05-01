using Shared.Base;

namespace Domain.Entities;

public class Milestone : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime DueDate { get; private set; }
    public bool IsCompleted { get; private set; }

    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; } = null!;

    private Milestone() { }

    public Milestone(string name, DateTime dueDate, Guid projectId, string? description = null)
    {
        Name = name;
        DueDate = dueDate;
        ProjectId = projectId;
        Description = description;
        IsCompleted = false;
    }

    public void MarkComplete()
    {
        IsCompleted = true;
    }

    public void Update(string name, string? description, DateTime dueDate)
    {
        Name = name;
        Description = description;
        DueDate = dueDate;
    }
}