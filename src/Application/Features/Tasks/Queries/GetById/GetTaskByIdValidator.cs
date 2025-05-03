using Application.Common.Validation;
using Shared.Constants;

namespace Application.Features.Tasks.Queries.GetById;

public class GetTaskByIdValidator : IValidator<GetTaskByIdQuery>
{
    public ValidationResult Validate(GetTaskByIdQuery request)
    {
        var result = new ValidationResult();
        
        if (request.Id == Guid.Empty)
        {
            result.AddError("Id", ValidationMessages.Common.TaskIdRequired);
        }

        return result;
    }
}