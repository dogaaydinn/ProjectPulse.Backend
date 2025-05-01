namespace Application.Features.Tasks.Commands;

public class MarkTaskAsCompleteCommandValidator : AbstractValidator<MarkTaskAsCompleteCommand>
{
    public MarkTaskAsCompleteCommandValidator()
    {
        RuleFor(x => x.TaskId)
            .NotEmpty()
            .WithMessage("Task ID is required.");
    }
}