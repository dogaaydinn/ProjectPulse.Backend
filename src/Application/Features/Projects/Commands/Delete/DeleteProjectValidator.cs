using Application.Common.Validation;
using Shared.Results.Errors;

namespace Application.Features.Projects.Commands.Delete;

public class DeleteProjectValidator : IValidator<DeleteProjectCommand>
{
    public ValidationResult Validate(DeleteProjectCommand request)
    {
        var result = new ValidationResult();

        result.IfEmptyGuid(request.Id, ProjectErrors.ProjectIdRequired);

        return result.IsValid ? ValidationResult.Success() : result;
    }
}