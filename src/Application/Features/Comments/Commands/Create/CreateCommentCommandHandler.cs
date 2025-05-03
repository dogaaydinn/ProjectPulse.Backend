using Domain.Factories;
using Domain.Repositories;
using Shared.Results;

namespace Application.Features.Comments.Commands.Create;

public class CreateCommentCommandHandler(
    ICommentRepository commentRepository,
    ICommentFactory commentFactory,
    IUnitOfWork unitOfWork)
{
    private readonly ICommentRepository _commentRepository = commentRepository;

    public async Task<Result<Guid>> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = commentFactory.Create(command.TaskId, command.AuthorId, command.Content);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(comment.Id);
    }
}