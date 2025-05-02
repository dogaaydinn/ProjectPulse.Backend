using Shared.Constants;

namespace Application.Features.Comments.Commands.Create;

public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentValidator()
    {
        RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage(ValidationMessages.TaskIdRequired);

        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage(ValidationMessages.AuthorIdRequired);

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage(ValidationMessages.CommentContentRequired)
            .MaximumLength(1000).WithMessage(ValidationMessages.CommentContentMaxLength);
    }
}