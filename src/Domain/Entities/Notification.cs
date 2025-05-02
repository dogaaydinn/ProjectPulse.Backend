using Shared.Base;
using Shared.Exceptions;

namespace Domain.Entities;

public class Notification : BaseEntity
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public string Message { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public bool IsRead { get; private set; }

    public string? SourceType { get; private set; }  // Optional: "Task", "Project", etc.
    public Guid? SourceId { get; private set; }

    protected Notification() { }

    public Notification(Guid userId, string message, string? sourceType = null, Guid? sourceId = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new AppException("Validation.Notification.Message", "Message cannot be empty.");

        UserId = userId;
        Message = message.Trim();
        CreatedAt = DateTime.UtcNow;
        IsRead = false;
        SourceType = sourceType;
        SourceId = sourceId;
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }
}