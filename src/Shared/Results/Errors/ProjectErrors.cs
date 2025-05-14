namespace Shared.Results.Errors;

public static class ProjectErrors
{
    public static Error NameRequired() =>
        Error.Validation("Project.NameRequired", "Project name cannot be empty.");

    public static Error ScheduleRequired() =>
        Error.Validation("Project.Schedule.Required", "Project schedule is required.");

    public static Error CreatedByRequired() =>
        Error.Validation("Project.CreatedBy.Required", "CreatedByUserId is required.");

    public static Error ManagerIdRequired() =>
        Error.Validation("Project.ManagerId.Required", "Project manager ID is required.");

    public static Error InvalidSchedule(DateTime start, DateTime end) =>
        Error.Validation("Project.Schedule.Invalid", $"Start date must be before end date: {start} → {end}.");

    public static Error ModelRequired() =>
        Error.Validation("Project.Model.Required", "Project model is required.");
    public static Error DescriptionTooLong(int maxLength) =>
        Error.Validation("Project.Description.TooLong", $"Description exceeds maximum length of {maxLength} characters.");
}