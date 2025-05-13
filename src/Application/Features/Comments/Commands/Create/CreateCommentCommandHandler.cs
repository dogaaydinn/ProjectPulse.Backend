using Application.Common.Handlers;
using Application.Common.Validation;
using Application.Features.Comments.Commands.Create;
using Domain.Core.Persistence;
using Domain.Factories;
using Domain.Modules.Tasks.Repositories;
using Shared.Results;

public class CreateCommentCommandHandler
    : BaseCommandHandler<CreateCommentCommand, Guid>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ICommentFactory _commentFactory;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCommentCommandHandler(
        ICommentRepository commentRepository,
        ICommentFactory commentFactory,
        IUnitOfWork unitOfWork,
        IValidator<CreateCommentCommand> validator)
        : base(validator)
    {
        _commentRepository = commentRepository;
        _commentFactory = commentFactory;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        return await ValidateAndExecuteAsync(command, async () =>
        {
            var comment = _commentFactory.Create(command.TaskId, command.AuthorId, command.Content);
            await _commentRepository.AddAsync(comment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(comment.Id);
        });
    }
}