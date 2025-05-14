using Domain.Modules.Tasks.Entities;
using Domain.Modules.Tasks.Enums;
using Shared.ValueObjects;

namespace Infrastructure.Factories;

public class TaskFactory
{
    public TaskItem Create(
        LocalizedString title,
        LocalizedString description,
        TaskPriority priority,
        TaskType type,
        Guid projectId,
        Guid? assigneeId,
        Guid? reporterId)
    {
        return new TaskItem(title, description, priority, type, projectId, assigneeId, reporterId);
    }

    public TaskItem Create(
        LocalizedString title,
        LocalizedString description,
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