
using Shared.Constants;

namespace Application.Features.Projects.Commands.Create;

public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidationMessages.ProjectIdRequired)
            .MaximumLength(100);

        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate ?? DateTime.MaxValue)
            .WithMessage(ValidationMessages.StartDateMustBeBeforeOrEqualToEndDate);

        RuleFor(x => x.ManagerId)
            .NotEmpty().WithMessage(ValidationMessages.ManagerIdRequired);
    }
}