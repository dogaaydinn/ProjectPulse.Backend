using Shared.Base;
using Shared.Exceptions;

namespace Domain.Entities;

public class Comment : BaseEntity
{
    public string Content { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; } = null!;

    public Guid AuthorId { get; private set; }
    public User Author { get; private set; } = null!;

    protected Comment() { }

    internal Comment(string content, Guid taskItemId, Guid authorId)
    {
        SetContent(content);
        TaskItemId = taskItemId;
        AuthorId = authorId;
        CreatedAt = DateTime.UtcNow;
    }

    private void SetContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new AppException("Validation.Comment.Content", "Comment cannot be empty.");

        Content = content.Trim();
    }
}