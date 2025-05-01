namespace Domain.Entities;

public class TaskLabel
{
    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; } = null!;

    public Guid LabelId { get; private set; }
    public Label Label { get; private set; } = null!;
}