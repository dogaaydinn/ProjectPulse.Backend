namespace Domain.Entities;

public class TaskDependency
{
    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; } = null!;

    public Guid DependsOnTaskId { get; private set; }
    public TaskItem DependsOnTask { get; private set; } = null!;

    public string DependencyType { get; private set; } = "FinishToStart"; // Enum olabilir

    private TaskDependency() { }

    public TaskDependency(Guid taskItemId, Guid dependsOnTaskId, string dependencyType = "FinishToStart")
    {
        if (taskItemId == dependsOnTaskId)
            throw new ArgumentException("A task cannot depend on itself.");

        TaskItemId = taskItemId;
        DependsOnTaskId = dependsOnTaskId;
        DependencyType = dependencyType;
    }

    public void UpdateDependencyType(string type)
    {
        DependencyType = type;
    }
}