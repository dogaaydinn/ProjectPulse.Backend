namespace Application.Features.Project.Commands;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Project name is required.")
            .MaximumLength(100);

        RuleFor(x => x.ManagerId)
            .NotEmpty().WithMessage("Manager ID is required.");

        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(DateTime.UtcNow.AddYears(1))
            .WithMessage("Start date cannot be more than 1 year in the future.");
    }
}