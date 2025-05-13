using Application.Common.Handlers;
using Application.DTOs;
using Application.DTOs.Comment;
using Domain.Modules.Tasks.Repositories;
using Shared.Results;

namespace Application.Features.Comments.Queries;

public class GetCommentByIdQueryHandler : BaseQueryHandler<GetCommentByIdQuery, CommentDto>
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentByIdQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Result<CommentDto>> Handle(GetCommentByIdQuery query, CancellationToken cancellationToken)
    {
        return await TryExecuteAsync(async () =>
        {
            var comment = await _commentRepository.GetByIdAsync(query.Id);

            if (comment is null)
                return Result<CommentDto>.Failure(Error.NotFound("Comment", query.Id));

            var dto = new CommentDto(
                comment.Id,
                comment.Content,
                comment.CreatedDate ?? default,
                comment.TaskItemId,
                comment.AuthorId
            );

            return Result<CommentDto>.Success(dto);
        });
    }
}