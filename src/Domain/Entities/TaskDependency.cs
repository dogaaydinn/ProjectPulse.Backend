using Domain.Enums;
using Shared.Base;
using Shared.Exceptions;

namespace Domain.Entities;

public class TaskDependency : BaseEntity
{
    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; } = null!;

    public Guid DependsOnTaskId { get; private set; }
    public TaskItem DependsOnTask { get; private set; } = null!;

    public DependencyType DependencyType { get; private set; } = DependencyType.FinishToStart;

    protected TaskDependency() { }

    internal TaskDependency(Guid taskItemId, Guid dependsOnTaskId, DependencyType dependencyType)
    {
        SetDependency(taskItemId, dependsOnTaskId);
        DependencyType = dependencyType;
    }

    private void SetDependency(Guid taskId, Guid dependsOnId)
    {
        if (taskId == dependsOnId)
            throw new AppException("TaskDependency.SelfDependency", "A task cannot depend on itself.");

        TaskItemId = taskId;
        DependsOnTaskId = dependsOnId;
    }

    public void UpdateDependencyType(DependencyType newType)
    {
        DependencyType = newType;
    }
}