using Application.DTOs;
using Domain.Repositories;
using Shared.Results;

namespace Application.Features.Comments.Queries;

public class GetCommentByIdQueryHandler
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentByIdQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Result<CommentDto>> Handle(GetCommentByIdQuery query, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(query.Id);

        if (comment is null)
        {
            return Result<CommentDto>.Failure(Error.NotFound("Comment", query.Id));
        }

        var dto = new CommentDto(
            comment.Id,
            comment.Content,
            comment.CreatedAt,
            comment.TaskItemId,
            comment.AuthorId
        );

        return Result<CommentDto>.Success(dto);
    }
}