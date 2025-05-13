using Application.Common.Validation;

namespace Application.Features.Projects.Queries.GetAll;

public class GetAllProjectsValidator : IValidator<GetAllProjectsQuery>
{
    public ValidationResult Validate(GetAllProjectsQuery request)
    {
        return ValidationResult.Success();
    }
}