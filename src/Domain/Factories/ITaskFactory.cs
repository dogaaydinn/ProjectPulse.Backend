using Domain.Modules.Tasks.Entities;
using Domain.Modules.Tasks.Enums;

namespace Domain.Factories;

public interface ITaskFactory
{
    TaskItem Create(
        string title,
        string? description,
        TaskPriority priority,
        TaskType type,
        Guid projectId,
        Guid? assigneeId = null,
        Guid? reporterId = null);
}