using Shared.Constants;

namespace Application.Features.Projects.Queries.GetById;

public class GetByIdValidator :AbstractValidator<GetProjectByIdQuery>
{
    public GetByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidationMessages.ProjectIdRequired);
    }
}