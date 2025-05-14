using Shared.Results;

namespace Application.Common.Validation.Errors;

public static class ProjectErrors
{
    public static Error NameRequired() =>
        Error.Validation("Project.NameRequired", "Project name cannot be empty.");
    
    public static Error DescriptionRequired() =>
        Error.Validation("Project.DescriptionRequired", "Project description cannot be empty.");
    
    public static Error ManagerRequired() =>
        Error.Validation("Project.ManagerRequired", "Project manager cannot be empty.");

    public static Error InvalidSchedule(DateTime start, DateTime end) =>
        Error.Validation("Project.Schedule.Invalid", $"Schedule is invalid: {start} → {end}.");
}