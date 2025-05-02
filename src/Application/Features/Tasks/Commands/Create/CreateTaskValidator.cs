using Application.Common.Validation;
using Shared.Constants;

namespace Application.Features.Tasks.Commands.Create;

public class CreateTaskValidator : IValidator<CreateTaskCommand>
{
    public ValidationResult Validate(CreateTaskCommand request)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(request.Title))
            result.AddError(ValidationMessages.TaskTitleRequired);

        if (request.ProjectId == Guid.Empty)
            result.AddError(ValidationMessages.ProjectIdRequired);

        if (request.StartDate.HasValue && request.EndDate.HasValue &&
            request.StartDate > request.EndDate)
        {
            result.AddError(ValidationMessages.StartDateMustBeBeforeEndDate);
        }

        return result;
    }
}