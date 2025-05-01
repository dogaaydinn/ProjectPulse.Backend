namespace Domain.Entities;

public class TaskAssignment
{
    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; } = null!;

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public string Role { get; private set; } = "Developer"; // Enum yapılabilir
    public DateTime AssignedAt { get; private set; }

    private TaskAssignment() { }

    public TaskAssignment(Guid taskItemId, Guid userId, string role)
    {
        TaskItemId = taskItemId;
        UserId = userId;
        Role = role;
        AssignedAt = DateTime.UtcNow;
    }
}