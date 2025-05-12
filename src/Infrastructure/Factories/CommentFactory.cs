using Domain.Factories;
using Domain.Modules.Tasks.Entities;

namespace Infrastructure.Factories;

public class CommentFactory : ICommentFactory
{
    public Comment Create(Guid taskId, Guid authorId, string content)
    {
        return new Comment(content, taskId, authorId);
    }
}