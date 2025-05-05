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
   
        if (!Enum.TryParse(command.Priority, true, out _))
            result.AddError("Priority", ValidationMessages.Common.InvalidPriority);
   
        if (!Enum.TryParse(command.Type, true, out _))
            result.AddError("Type", ValidationMessages.Common.InvalidType);
   
        if (command.ProjectId == Guid.Empty)
            result.AddError("ProjectId", ValidationMessages.Common.ProjectRequired);
   
        return result;
    }
}
