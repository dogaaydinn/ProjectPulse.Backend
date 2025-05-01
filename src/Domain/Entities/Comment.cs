using Shared.Base;

namespace Domain.Entities;

public class Comment : BaseEntity
{
    public string Content { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; } = null!;

    public Guid AuthorId { get; private set; }
    public User Author { get; private set; } = null!;

    private Comment() { }

    public Comment(string content, Guid taskItemId, Guid authorId)
    {
        Content = content;
        TaskItemId = taskItemId;
        AuthorId = authorId;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string content)
    {
        Content = content;
    }
}