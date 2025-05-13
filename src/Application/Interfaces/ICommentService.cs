using Application.DTOs;
using Application.DTOs.Comment;
using Shared.Results;

namespace Application.Interfaces;

public interface ICommentService
{
    Task<Result<Guid>> CreateCommentAsync(CreateCommentRequest request);
    Task<Result<CommentDto>> GetCommentByIdAsync(Guid commentId);
    Task<Result<List<CommentDto>>> GetCommentsByTaskIdAsync(Guid taskId);
    Task<Result> DeleteCommentAsync(Guid commentId);
}