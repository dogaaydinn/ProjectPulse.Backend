using Shared.Constants;

namespace Application.Features.Projects.Commands.Create;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.ProjectNameRequired);
        
        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate ?? DateTime.MaxValue)
            .WithMessage(ValidationMessages.StartDateMustBeBeforeEndDate);
        
        //ManagerId must be provided.
        RuleFor(x => x.ManagerId)
            .NotEmpty()
            .WithMessage(ValidationMessages.ManagerIdRequired);
        
    }
}