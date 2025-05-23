namespace Shared.Results.Errors.Domain;

public static class ProjectErrorExtensions
{
    private const string Prefix = "Project";

    public static Error NotFound(this IErrorFactory factory, Guid projectId) =>
        factory.Create(
            code: $"{Prefix}.NotFound",
            args: new object[] { projectId },
            metadata: new Dictionary<string, object> { ["ProjectId"] = projectId },
            severity: ErrorSeverity.High);

    public static Error InvalidStatusTransition(this IErrorFactory factory, string current, string target) =>
        factory.Create(
            code: $"{Prefix}.InvalidStatusTransition",
            args: new object[] { current, target },
            metadata: new Dictionary<string, object>
            {
                ["CurrentStatus"] = current,
                ["TargetStatus"]  = target
            },
            severity: ErrorSeverity.Medium);

    public static Error ScheduleMissing(this IErrorFactory factory, Guid projectId) =>
        factory.Create(
            code: $"{Prefix}.Schedule.Missing",
            args: new object[] { projectId },
            metadata: new Dictionary<string, object> { ["ProjectId"] = projectId },
            severity: ErrorSeverity.Medium);

    public static Error DescriptionTooLong(this IErrorFactory factory, int maxLength) =>
        factory.Create(
            code: $"{Prefix}.Description.TooLong",
            args: new object[] { maxLength },
            metadata: new Dictionary<string, object> { ["MaxLength"] = maxLength },
            severity: ErrorSeverity.Low);

    public static Error NameRequired(this IErrorFactory factory) =>
        factory.Create(
            code: $"{Prefix}.NameRequired",
            severity: ErrorSeverity.Low);

    public static Error ManagerIdRequired(this IErrorFactory factory) =>
        factory.Create(
            code: $"{Prefix}.ManagerId.Required",
            severity: ErrorSeverity.Low);

    public static Error InvalidSchedule(this IErrorFactory factory, DateTime start, DateTime end) =>
        factory.Create(
            code: $"{Prefix}.Schedule.Invalid",
            args: new object[] { start, end },
            metadata: new Dictionary<string, object>
            {
                ["Start"] = start,
                ["End"]   = end
            },
            severity: ErrorSeverity.Low);
}