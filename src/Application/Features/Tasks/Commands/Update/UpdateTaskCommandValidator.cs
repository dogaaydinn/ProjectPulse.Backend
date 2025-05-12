using Application.Common.Validation;
using Application.DTOs;
using Shared.Constants;

namespace Application.Features.Tasks.Commands.Update;

public class UpdateTaskCommandValidator : IValidator<UpdateTaskCommand>
{
    public ValidationResult Validate(UpdateTaskCommand command)
    {
        var result = new ValidationResult();

        if (command.Id == Guid.Empty)
            result.AddError("TaskId", ValidationMessages.Common.TaskIdRequired);

        if (string.IsNullOrWhiteSpace(command.Title))
            result.AddError("Title", ValidationMessages.Task.TitleRequired);
        
        return result;
    }
}