using Domain.Factories;
using Domain.Repositories;
using Shared.Results;

namespace Application.Features.Comments.Commands.Create;

public class CreateCommentCommandHandler
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICommentFactory _commentFactory;

    public CreateCommentCommandHandler(
        ICommentRepository commentRepository,
        ICommentFactory commentFactory,
        IUnitOfWork unitOfWork)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
        _commentFactory = commentFactory;
    }

    public async Task<Result<Guid>> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = _commentFactory.Create(command.TaskId, command.AuthorId, command.Content);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(comment.Id);
    }
}