using Application.Common.Validation;
using Shared.Constants;

namespace Application.Features.Projects.Commands.Create;

public class CreateProjectValidator : IValidator<CreateProjectCommand>
{
    public ValidationResult Validate(CreateProjectCommand request)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(request.Name))
            result.AddError(ValidationMessages.ProjectNameRequired);

        if (request.StartDate > (request.EndDate ?? DateTime.MaxValue))
            result.AddError(ValidationMessages.StartDateMustBeBeforeEndDate);

        if (request.ManagerId == Guid.Empty)
            result.AddError(ValidationMessages.ManagerIdRequired);

        return result;
    }
}