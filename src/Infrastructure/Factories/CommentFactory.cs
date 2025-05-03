using Domain.Entities;
using Domain.Factories;

namespace Infrastructure.Factories;

public class CommentFactory : ICommentFactory
{
    public Comment Create(Guid taskId, Guid authorId, string content)
    {
        return new Comment(content, taskId, authorId);
    }
}