using Shared.Base;

namespace Domain.Entities;

public class Status : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Color { get; private set; } = "#000000";
    public bool IsClosed { get; private set; }

    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; } = null!;

    public ICollection<TaskItem> Tasks { get; private set; } = new List<TaskItem>();

    private Status() { }

    public Status(string name, string color, bool isClosed, Guid projectId)
    {
        Name = name;
        Color = color;
        IsClosed = isClosed;
        ProjectId = projectId;
    }

    public void Update(string name, string color, bool isClosed)
    {
        Name = name;
        Color = color;
        IsClosed = isClosed;
    }
}