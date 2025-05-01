using Domain.Enums;

namespace Domain.Entities;

public class TaskDependency
{
    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; } = null!;

    public Guid DependsOnTaskId { get; private set; }
    public TaskItem DependsOnTask { get; private set; } = null!;

    public DependencyType DependencyType { get; private set; } = DependencyType.FinishToStart;
    private TaskDependency() { }

    public TaskDependency(Guid taskItemId, Guid dependsOnTaskId, DependencyType dependencyType = DependencyType.FinishToStart)
    {
        if (taskItemId == dependsOnTaskId)
            throw new ArgumentException("A task cannot depend on itself.");

        TaskItemId = taskItemId;
        DependsOnTaskId = dependsOnTaskId;
        DependencyType = dependencyType;
    }

    public void UpdateDependencyType(DependencyType type)
    {
        DependencyType = type;
    }
}