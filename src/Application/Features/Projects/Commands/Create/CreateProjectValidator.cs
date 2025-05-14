using Application.Common.Validation;
using Shared.Results.Errors;

namespace Application.Features.Projects.Commands.Create;

public class CreateProjectValidator : IValidator<CreateProjectCommand>
{
    public ValidationResult Validate(CreateProjectCommand request)
    {
        var result = new ValidationResult();

        result.IfEmptyLocalized(request.Name, ProjectErrors.NameRequired);
        result.IfEmptyDateRange(request.Schedule, ProjectErrors.ScheduleRequired);
        result.IfEndBeforeStart(
            request.Schedule.Start,
            request.Schedule.End,
            () => ProjectErrors.InvalidSchedule(request.Schedule.Start, request.Schedule.End)
        );
        result.IfEmptyGuid(request.ManagerId, ProjectErrors.ManagerIdRequired);

        return result.IsValid ? ValidationResult.Success() : result;
    }
}