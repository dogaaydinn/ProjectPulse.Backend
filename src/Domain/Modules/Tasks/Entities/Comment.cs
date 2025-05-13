using Domain.Modules.Users.Entities;
using Shared.Base;
using Shared.Constants;
using Shared.Validation;

namespace Domain.Modules.Tasks.Entities;

public class Comment : BaseAuditableEntity
{
    public string Content { get; private set; } = string.Empty;

    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; } = null!;

    public Guid AuthorId { get; private set; }
    public User Author { get; private set; } = null!;

    protected Comment() { }

    public Comment(string content, Guid taskItemId, Guid authorId)
    {
        SetContent(content);
        Guard.AgainstDefaultGuid(taskItemId, ErrorCodes.Validation, ValidationMessages.Comment.TaskRequired);
        Guard.AgainstDefaultGuid(authorId, ErrorCodes.Validation, ValidationMessages.Comment.AuthorRequired);

        TaskItemId = taskItemId;
        AuthorId = authorId;
    }

    public void SetContent(string content)
    {
        Guard.AgainstEmpty(content, ErrorCodes.Validation, ValidationMessages.Comment.ContentRequired);
        Content = content.Trim();
    }
}