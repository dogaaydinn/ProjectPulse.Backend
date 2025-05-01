using Shared.Base;

namespace Domain.Entities;

public class Label : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Color { get; private set; } = "#000000";

    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; } = null!;

    public ICollection<TaskLabel> TaskLabels { get; private set; } = new List<TaskLabel>();

    private Label() { }

    public Label(string name, string color, Guid projectId)
    {
        Name = name;
        Color = color;
        ProjectId = projectId;
    }

    public void Update(string name, string color)
    {
        Name = name;
        Color = color;
    }
}