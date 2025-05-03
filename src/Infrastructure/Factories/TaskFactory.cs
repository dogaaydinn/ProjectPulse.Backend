using Domain.Entities;
using Domain.Enums;
using Domain.Factories;

namespace Infrastructure.Factories;

public class TaskFactory : ITaskFactory
{
    public TaskItem Create(
        string title,
        string? description,
        TaskPriority priority,
        TaskType type,
        Guid projectId,
        Guid? assigneeId = null,
        Guid? reporterId = null)
    {
        var task = new TaskItem(title, description, priority, type, projectId, assigneeId, reporterId);
        return task;
    }
}