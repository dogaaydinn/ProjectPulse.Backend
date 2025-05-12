using Domain.Modules.Tasks.Entities;

namespace Domain.Factories;

public interface ICommentFactory
{
    Comment Create(Guid taskId, Guid authorId, string content);
}