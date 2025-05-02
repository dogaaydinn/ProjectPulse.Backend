using Shared.Base;
using Shared.Exceptions;

namespace Domain.Entities;

public class Label : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Color { get; private set; }

    public ICollection<TaskLabel> TaskLabels { get; private set; } = new List<TaskLabel>();

    protected Label() { }

    public Label(string name, string? color = null)
    {
        SetName(name);
        Color = color;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new AppException("Validation.Label.Name", "Label name cannot be empty.");

        Name = name.Trim();
    }

    public void ChangeColor(string? color)
    {
        Color = color;
    }

    public void Rename(string newName)
    {
        SetName(newName);
    }
}