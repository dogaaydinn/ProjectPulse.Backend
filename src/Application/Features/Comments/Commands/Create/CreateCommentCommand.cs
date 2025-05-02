namespace Application.Features.Comments.Commands.Create;

public record CreateCommentCommand(
    Guid TaskId,
    Guid AuthorId,
    string Content
);