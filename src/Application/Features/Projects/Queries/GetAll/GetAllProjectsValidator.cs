using Application.Common.Validation;

namespace Application.Features.Projects.Queries.GetAll;

public class GetAllProjectsValidator : IValidator<GetAllProjectsQuery>
{
    // This class is empty because the GetAllProjectsQuery does not have any specific validation rules.
    public ValidationResult Validate(GetAllProjectsQuery request)
    {
        throw new NotImplementedException();
    }
}