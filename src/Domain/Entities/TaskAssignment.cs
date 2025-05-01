using Domain.Enums;

namespace Domain.Entities;

public class TaskAssignment
{
    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; } = null!;

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public TaskRole Role { get; private set; } = TaskRole.Developer;
    public DateTime AssignedAt { get; private set; }

    private TaskAssignment() { }

    public TaskAssignment(Guid taskItemId, Guid userId, TaskRole role)
    {
        TaskItemId = taskItemId;
        UserId = userId;
        Role = role;
        AssignedAt = DateTime.UtcNow;
    }
}