using Application.Common.Validation;
using Shared.Constants;

namespace Application.Features.Tasks.Commands.Create;

public class CreateTaskValidator : IValidator<CreateTaskCommand>
{
    public ValidationResult Validate(CreateTaskCommand command)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(command.Title))
            result.AddError("Title", ValidationMessages.Task.TitleRequired);
        

        if (command.ProjectId == Guid.Empty)
            result.AddError("ProjectId", ValidationMessages.Common.ProjectIdRequired);

        return result;
    }
}