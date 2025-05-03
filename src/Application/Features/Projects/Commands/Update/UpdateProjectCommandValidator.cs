using Application.Common.Validation;
using Shared.Constants;

namespace Application.Features.Projects.Commands.Update;

public class UpdateProjectCommandValidator : IValidator<UpdateProjectCommand>
{
    public ValidationResult Validate(UpdateProjectCommand command)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(command.Name))
            result.AddError("Name", ValidationMessages.Project.ProjectNameRequired);

        if (command.ManagerId == Guid.Empty)
            result.AddError("ManagerId", ValidationMessages.Project.ManagerIdRequired);

        return result;
    }
}