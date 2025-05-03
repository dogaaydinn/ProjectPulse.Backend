using Application.Common.Validation;
using Shared.Constants;

namespace Application.Features.Comments.Commands.Create;

public class CreateCommentValidator : IValidator<CreateCommentCommand>
{
    public ValidationResult Validate(CreateCommentCommand request)
    {
        var result = new ValidationResult();

        if (request.TaskId == Guid.Empty)
            result.AddError("TaskId", ValidationMessages.TaskIdRequired);

        if (request.AuthorId == Guid.Empty)
            result.AddError("AuthorId", ValidationMessages.AuthorIdRequired);

        if (string.IsNullOrWhiteSpace(request.Content))
            result.AddError("Content", ValidationMessages.CommentContentRequired);

        return result;
    }
}