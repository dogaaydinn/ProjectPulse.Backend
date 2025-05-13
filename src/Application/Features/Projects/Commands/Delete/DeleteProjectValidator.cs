using Application.Common.Validation;
using Shared.Constants;

namespace Application.Features.Projects.Commands.Delete;

public class DeleteProjectValidator : IValidator<DeleteProjectCommand>
{
    public ValidationResult Validate(DeleteProjectCommand request)
    {
        var result = new ValidationResult();
        result.IfEmptyGuid(request.Id, nameof(request.Id), ValidationMessages.Project.ProjectIdRequired);
        return result.IsValid ? ValidationResult.Success() : result;
    }
}