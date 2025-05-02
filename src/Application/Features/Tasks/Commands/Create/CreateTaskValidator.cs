using Shared.Constants;

namespace Application.Features.Tasks.Commands.Create;

public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(ValidationMessages.TaskTitleRequired)
            .MaximumLength(100);

        RuleFor(x => x.ProjectId)
            .NotEmpty().WithMessage(ValidationMessages.ProjectIdRequired);

        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate ?? DateTime.MaxValue)
            .WithMessage(ValidationMessages.StartDateMustBeBeforeOrEqualToEndDate);
    }
}