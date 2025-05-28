using System.Diagnostics;
using Shared.Abstractions.Localization;
using Shared.Constants;

namespace Shared.Results;

public class ErrorFactory : IErrorFactory
{
    private readonly IErrorRegistry _errorRegistry;
    private readonly IErrorLocalizer? _localizer;

    public ErrorFactory(IErrorRegistry errorRegistry, IErrorLocalizer? localizer = null)
    {
        _errorRegistry = errorRegistry;
        _localizer = localizer;
    }

    public Error Create(
        string code,
        object[]? args = null,
        Dictionary<string, object>? metadata = null,
        ErrorSeverity? severity = null,
        ErrorCategory? category = null)
    {
        var definition = _errorRegistry.Get(code);

        if (definition.IsDeprecated)
        {
            Debug.WriteLine($"[WARNING] Deprecated error used: {code}. {definition.DeprecationMessage}");
        }

        var messageTemplate = _localizer?.GetMessage(code, args ?? []) ?? definition.MessageTemplate;

        return new Error(
            code: code,
            messageTemplate: messageTemplate,
            args: args,
            metadata: metadata,
            severity: severity ?? definition.Severity,
            category: category ?? definition.Category
        );
    }

    public Error Unexpected(string message) =>
        Create(
            code: ErrorCodes.General.Unexpected,
            args: new[] { message },
            metadata: new() { ["Exception"] = message },
            severity: ErrorSeverity.Critical,
            category: ErrorCategory.Unexpected
        );

    public Error Conflict(string message, object? context = null) =>
        Create(
            code: ErrorCodes.General.Conflict,
            args: new[] { message },
            metadata: new() { ["Context"] = context ?? "Unknown" },
            severity: ErrorSeverity.Warning,
            category: ErrorCategory.Conflict
        );

    public Error Validation(string field, string rule, object? invalidValue) =>
        Create(
            code: $"Validation.{field}.{rule}",
            args: new object[] { field, invalidValue ?? "null" },
            metadata: new() { ["Field"] = field, ["InvalidValue"] = invalidValue ?? "null", ["Rule"] = rule },
            category: ErrorCategory.Validation
        );

    public Error NotFound(string entity, object id) =>
        Create(
            code: $"{entity}.NotFound",
            args: [id],
            metadata: new() { ["Entity"] = entity, ["Id"] = id },
            category: ErrorCategory.NotFound
        );
    public Error Required(string fieldName) =>
        Create(
            code: $"Validation.{fieldName}.Required",
            args: new object[] { fieldName },
            metadata: new() { ["Field"] = fieldName },
            category: ErrorCategory.Validation
        );

    public Error Invalid(string fieldName, IEnumerable<string> validValues) =>
        Create(
            code: $"Validation.{fieldName}.Invalid",
            args: new object[] { fieldName, string.Join(", ", validValues) },
            metadata: new() { ["Field"] = fieldName, ["ValidValues"] = validValues },
            category: ErrorCategory.Validation
        );
}
