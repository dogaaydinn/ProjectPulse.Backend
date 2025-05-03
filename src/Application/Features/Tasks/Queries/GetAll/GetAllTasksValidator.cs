
using Application.Common.Validation;

namespace Application.Features.Tasks.Queries.GetAll;

public class GetAllTasksValidator : IValidator<GetAllTasksQuery>
{
    // This class is empty because the GetAllTasksQuery does not have any specific validation rules.
    public ValidationResult Validate(GetAllTasksQuery request)
    {
        // No validation rules for GetAllTasksQuery
        return new ValidationResult();
    }
}