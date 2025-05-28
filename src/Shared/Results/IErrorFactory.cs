namespace Shared.Results;

public interface IErrorFactory
{
    Error Create(
        string code,
        object[]? args = null,
        Dictionary<string, object>? metadata = null,
        ErrorSeverity? severity = null,
        ErrorCategory? category = null);

    Error NotFound(string entity, object id);
    Error Validation(string field, string rule, object? invalidValue);
    Error Conflict(string message, object? context = null);
    Error Unexpected(string message);
    Error Required(string fieldName);
    Error Invalid(string fieldName, IEnumerable<string> validValues);
}