using Application.Common.Validation;
using Shared.Constants;


namespace Application.Features.Projects.Commands.Create;

public class CreateProjectValidator : IValidator<CreateProjectCommand>
{
    public ValidationResult Validate(CreateProjectCommand request)
    {
        var result = new ValidationResult();

        result.IfEmptyLocalized(request.Name, nameof(request.Name), ValidationMessages.Project.ProjectNameRequired);
        result.IfNull(request.Schedule, nameof(request.Schedule), ValidationMessages.Common.ScheduleRequired);
        result.IfTrue(
            request.Schedule.End < request.Schedule.Start,
            nameof(request.Schedule),
            ValidationMessages.Common.StartDateMustBeBeforeEndDate
        );
        result.IfEmptyGuid(request.ManagerId, nameof(request.ManagerId), ValidationMessages.Project.ManagerIdRequired);

        return result.IsValid ? ValidationResult.Success() : result;
    }
}