using Application.Common.Validation;
using Shared.Constants;

namespace Application.Features.Tasks.Commands.Create;

public class CreateTaskValidator : IValidator<CreateTaskCommand>
{
    public ValidationResult Validate(CreateTaskCommand request)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(request.Title))
            result.AddError("Title", ValidationMessages.TaskTitleRequired);
        if (request.ProjectId == Guid.Empty)
            result.AddError("ProjectId", ValidationMessages.ProjectIdRequired);

        if (request.StartDate.HasValue && request.EndDate.HasValue &&
            request.StartDate > request.EndDate)
        {
            result.AddError("StartDate", ValidationMessages.StartDateMustBeBeforeEndDate);
        }

        return result;
    }
}
/*
public ValidationResult Validate(CreateTaskCommand command)
       {
           var result = new ValidationResult();
   
           if (string.IsNullOrWhiteSpace(command.Title))
               result.AddError("Title", ValidationMessages.Task.TitleRequired);
   
           if (!Enum.TryParse(command.Priority, true, out _))
               result.AddError("Priority", ValidationMessages.Task.InvalidPriority);
   
           if (!Enum.TryParse(command.Type, true, out _))
               result.AddError("Type", ValidationMessages.Task.InvalidType);
   
           if (command.ProjectId == Guid.Empty)
               result.AddError("ProjectId", ValidationMessages.Task.ProjectRequired);
   
           return result;
       }
   }
   */
   