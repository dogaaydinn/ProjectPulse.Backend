using Domain.Modules.Users.Enums;
using Shared.Base;
using Shared.Exceptions;
using Shared.Validation;

namespace Domain.Modules.Users.Entities;

public class Notification : BaseEntity
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public string Message { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public bool IsRead { get; private set; }

    public NotificationType? Type { get; private set; }
    public Guid? SourceId { get; private set; }

    protected Notification() { }

    public Notification(Guid userId, string message, NotificationType? type = null, Guid? sourceId = null)
    {
        Guard.AgainstDefaultGuid(userId, "Validation.Notification.User", "UserId is required.");
        Guard.AgainstEmpty(message, "Validation.Notification.Message", "Message cannot be empty.");

        UserId = userId;
        Message = message.Trim();
        CreatedAt = DateTime.UtcNow;
        IsRead = false;
        Type = type;
        SourceId = sourceId;
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }
}