using Shared.Constants;

namespace Application.Features.Projects.Queries.GetById;

public class GetProjectByIdValidator :AbstractValidator<GetProjectByIdQuery>
{
    public GetProjectByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidationMessages.ProjectIdRequired);
    }
}