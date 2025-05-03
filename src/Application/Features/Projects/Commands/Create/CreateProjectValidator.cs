using Application.Common.Validation;
using Shared.Constants;

namespace Application.Features.Projects.Commands.Create;

public class CreateProjectValidator : IValidator<CreateProjectCommand>
{
    public ValidationResult Validate(CreateProjectCommand request)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(request.Name))
            result.AddError("Name", ValidationMessages.Project.ProjectNameRequired);

        if (request.StartDate > (request.EndDate ?? DateTime.MaxValue))
            result.AddError("StartDate", ValidationMessages.Common.StartDateMustBeBeforeEndDate);

        if (request.ManagerId == Guid.Empty)
            result.AddError("ManagerId", ValidationMessages.Project.ManagerIdRequired);

        return result;
    }
}