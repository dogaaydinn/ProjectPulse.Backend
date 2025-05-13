using Application.Common.Validation;
using Shared.Constants;

namespace Application.Features.Projects.Commands.Update;

public class UpdateProjectCommandValidator : IValidator<UpdateProjectCommand>
{
    public ValidationResult Validate(UpdateProjectCommand command)
    {
        var result = new ValidationResult();

        result.IfEmptyLocalized(command.Name, nameof(command.Name), ValidationMessages.Project.ProjectNameRequired);
        result.IfEmptyLocalized(command.Description, nameof(command.Description), ValidationMessages.Project.ProjectDescriptionRequired);

        result.IfEmptyGuid(command.ManagerId, nameof(command.ManagerId), ValidationMessages.Project.ManagerIdRequired);
        
        return result is { IsValid: true }
            ? ValidationResult.Success()
            : result;
    }
}