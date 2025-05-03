using Application.DTOs;
using Domain.Repositories;
using Shared.Results;

namespace Application.Features.Comments.Queries;

public class GetCommentsByTaskIdQueryHandler(ICommentRepository commentRepository)
{
    public async Task<Result<List<CommentDto>>> Handle(GetCommentsByTaskIdQuery query, CancellationToken cancellationToken)
    {
        var comments = await commentRepository.GetByTaskIdAsync(query.TaskItemId);

        var dtos = comments
            .Select(c => new CommentDto(
                c.Id,
                c.Content,
                c.CreatedAt,
                c.TaskItemId,
                c.AuthorId))
            .ToList();

        return Result<List<CommentDto>>.Success(dtos);
    }
}