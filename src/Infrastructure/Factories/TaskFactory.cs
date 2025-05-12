using Domain.Modules.Tasks.Entities;
using Domain.Modules.Tasks.Enums;

namespace Infrastructure.Factories;

public class TaskFactory
{
    public TaskItem Create(
        string title,
        string? description,
        TaskPriority priority,
        TaskType type,
        Guid projectId,
        Guid? assigneeId,
        Guid? reporterId)
    {
        return new TaskItem(title, description, priority, type, projectId, assigneeId, reporterId);
    }

    public TaskItem Create(
        string title,
        string? description,
        string priorityName,
        string typeName,
        Guid projectId,
        Guid? assigneeId,
        Guid? reporterId)
    {
        var priority = TaskPriority.ConvertOrThrow(priorityName);
        var type = TaskType.ConvertOrThrow(typeName);

        return new TaskItem(title, description, priority, type, projectId, assigneeId, reporterId);
    }
}