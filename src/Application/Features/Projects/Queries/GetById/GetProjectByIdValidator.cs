using Application.Common.Validation;
using Shared.Constants;

namespace Application.Features.Projects.Queries.GetById;

public class GetProjectByIdValidator : IValidator<GetProjectByIdQuery>
{
    public ValidationResult Validate(GetProjectByIdQuery request)
    {
        var result = new ValidationResult();

        if (request.ProjectId == Guid.Empty)
        {
            result.AddError("ProjectId", ValidationMessages.ProjectIdRequired);
        }

        return result;
    }
}