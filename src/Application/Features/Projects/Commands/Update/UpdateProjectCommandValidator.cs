using Application.Common.Validation;
using Shared.Constants;

namespace Application.Features.Projects.Commands.Update;

public class UpdateProjectCommandValidator : IValidator<UpdateProjectCommand>
{
    public ValidationResult Validate(UpdateProjectCommand command)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(command.Name))
            result.AddError("Name", ValidationMessages.ProjectNameRequired);

        if (command.ManagerId == Guid.Empty)
            result.AddError("ManagerId", ValidationMessages.ManagerIdRequired);

        return result;
    }
}