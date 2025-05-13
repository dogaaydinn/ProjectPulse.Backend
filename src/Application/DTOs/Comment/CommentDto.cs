namespace Application.DTOs.Comment;

public record CommentDto(
    Guid Id,
    string Content,
    DateTime CreatedAt,
    Guid TaskItemId,
    Guid AuthorId
);