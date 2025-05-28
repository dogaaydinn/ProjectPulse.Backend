using Shared.Constants;

namespace Shared.Results;

public class FakeErrorFactory : IErrorFactory
{
    public Error Create(
        string code,
        object[]? args = null,
        Dictionary<string, object>? metadata = null,
        ErrorSeverity? severity = null,
        ErrorCategory? category = null)
    {
        var cat = category ?? ErrorCategory.Domain;
        var sev = severity ?? ErrorSeverity.Medium;
        var template = $"[Fake] {code}";

        var err = Error.Create(
            code,
            template,
            category: cat,
            severity: sev,
            exception: null,
            args: args ?? []);

        return metadata == null ? err : metadata.Aggregate(err, (current, kv) => current.WithMetadata(kv.Key, kv.Value));
    }

    public Error Unexpected(string message) =>
        Create(
            ErrorCodes.General.Unexpected,
            args: [message],
            severity: ErrorSeverity.Critical,
            category: ErrorCategory.Infrastructure);

    public Error Conflict(string message, object? context = null) =>
        Create(
            ErrorCodes.General.Conflict,
            args: context != null ? [context] : [],
            severity: ErrorSeverity.Conflict,
            category: ErrorCategory.Conflict);

    public Error NotFound(string entity, object id) =>
        Create(
            $"{entity}.NotFound",
            args: [id],
            severity: ErrorSeverity.Low,
            category: ErrorCategory.NotFound);

    public Error Validation(string field, string rule, object? invalidValue) =>
        Create(
            $"Validation.{field}.{rule}",
            args: [field, invalidValue!],
            severity: ErrorSeverity.Validation,
            category: ErrorCategory.Validation);
    
    public Error Required(string fieldName) =>
        Create(
            $"Validation.{fieldName}.Required",
            args: [fieldName],
            severity: ErrorSeverity.Validation,
            category: ErrorCategory.Validation);
    public Error Invalid(string fieldName, IEnumerable<string> validValues) =>
        Create(
            $"Validation.{fieldName}.Invalid",
            args: [fieldName, string.Join(", ", validValues)],
            severity: ErrorSeverity.Validation,
            category: ErrorCategory.Validation);
}