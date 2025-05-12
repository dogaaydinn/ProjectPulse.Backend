using Domain.Modules.Tasks.Enums;
using Domain.Modules.Users.Entities;
using Shared.Base;
using Shared.Exceptions;

namespace Domain.Modules.Tasks.Entities;

public class TaskAssignment : BaseEntity
{
    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; } = null!;

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public TaskRole Role { get; private set; }
    public DateTime AssignedAt { get; private set; }

    protected TaskAssignment() { }

    internal TaskAssignment(Guid taskItemId, Guid userId, TaskRole role)
    {
        if (taskItemId == Guid.Empty || userId == Guid.Empty)
            throw new AppException("Validation.TaskAssignment", "Task or User ID cannot be empty.");

        TaskItemId = taskItemId;
        UserId = userId;
        Role = role;
        AssignedAt = DateTime.UtcNow;
    }

    public void UpdateRole(TaskRole newRole)
    {
        if (Role == newRole)
            return;

        Role = newRole;
    }
}