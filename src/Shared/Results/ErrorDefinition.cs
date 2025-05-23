namespace Shared.Results;

public sealed record ErrorDefinition(
    string Code,
    string MessageTemplate,
    ErrorSeverity Severity,
    ErrorCategory Category,
    bool IsDeprecated = false,
    string? DeprecationMessage = null,
    string? ReplacementCode = null,
    DateTime? DeprecationDate = null)
{
    public bool ShouldLogAsWarning => IsDeprecated || Severity == ErrorSeverity.Low;
}