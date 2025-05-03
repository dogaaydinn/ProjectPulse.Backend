namespace Application.Features.Comments.Queries;

public class GetCommentsByTaskIdQuery
{
    public Guid TaskItemId { get; init; }
}