using Shared.Constants;

namespace Application.Features.Tasks.Queries.GetById;

public class GetTaskByIdValidator : AbstractValidator<GetTaskByIdQuery>
{
    public GetTaskByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidationMessages.TaskIdRequired);
    }
}