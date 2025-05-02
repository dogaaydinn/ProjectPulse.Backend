using Shared.Constants;

namespace Application.Features.Projects.Commands.Create;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(ValidationMessages.ProjectTitleRequired)
            .MaximumLength(100);

        RuleFor(x => x.ProjectId)
            .NotEmpty().WithMessage(ValidationMessages.ProjectIdRequired);

        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate ?? DateTime.MaxValue)
            .WithMessage(ValidationMessages.StartDateMustBeBeforeOrEqualToEndDate);
    }
}