using Shared.Constants;

namespace Application.Features.Project.Commands;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.ProjectNameRequired);
        
    }
}