using System.Globalization;
using System.Text;
using Shared.Abstractions.Localization;

namespace Shared.Results;

public sealed record Error
{
    public string Code { get; init; }
    private string MessageTemplate { get; init; }
    private object[] MessageArgs { get; init; } = [];
    public IReadOnlyDictionary<string, object> Metadata { get; private init; } = new Dictionary<string, object>().AsReadOnly();
    public ErrorSeverity Severity { get; init; } = ErrorSeverity.Medium;
    public ErrorCategory Category { get; init; } = ErrorCategory.Domain;
    public Exception? Exception { get; init; }
    public string? HelpLink { get; init; }
    // Static factory methods
    public static Error Validation(string code, string message, Exception? ex = null, params object[] args)
        => new(code, message, args, category: ErrorCategory.Validation, exception: ex);

    public static Error Infrastructure(string code, string message, Exception? ex = null, params object[] args)
        => new(code, message, args, category: ErrorCategory.Infrastructure, exception: ex);

    public static Error Security(string code, string message, Exception? ex = null, params object[] args)
        => new(code, message, args, category: ErrorCategory.Security, exception: ex);

    public static Error NotFound(string code, string message, Exception? ex = null, params object[] args)
        => new(code, message, args, category: ErrorCategory.NotFound, exception: ex);
    public static Error Create(
        string code,
        string message,
        ErrorCategory category,
        ErrorSeverity severity,
        Exception? exception = null,
        params object[] args)
        => new(code, message, args, category: category, severity: severity, exception: exception);

    public Error(
        string code,
        string messageTemplate,
        object[]? args = null,
        Dictionary<string, object>? metadata = null,
        ErrorSeverity? severity = null,
        ErrorCategory? category = null,
        Exception? exception = null)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        MessageTemplate = messageTemplate ?? throw new ArgumentNullException(nameof(messageTemplate));
        MessageArgs = args ?? [];
        Metadata = metadata?.AsReadOnly() ?? new Dictionary<string, object>().AsReadOnly();
        Severity = severity ?? ErrorSeverity.Medium;
        Category = category ?? ErrorCategory.Domain;
        Exception = exception;
    }

    public string GetLocalizedMessage(IErrorLocalizer? localizer = null)
    {
        var msg = localizer?.GetMessage(Code, MessageArgs)
                  ?? string.Format(CultureInfo.CurrentCulture, MessageTemplate, MessageArgs);
        return Metadata.Aggregate(msg, (current, kv) => current.Replace($"{{{kv.Key}}}", kv.Value?.ToString()));
    }

    public Error WithMetadata(string key, object value)
    {
        var dict = Metadata.ToDictionary(kv => kv.Key, kv => kv.Value);
        dict[key] = value;
        return this with { Metadata = dict.AsReadOnly() };
    }

    public override string ToString()
    {
        var sb = new StringBuilder($"[{Code}] {MessageTemplate}");
        if (MessageArgs.Length > 0)
            sb.Append($" Args:[{string.Join(", ", MessageArgs)}]");
        if (Metadata.Count > 0)
            sb.Append($" Meta:{{{string.Join(", ", Metadata.Select(kv => $"{kv.Key}:{kv.Value}"))}}}");
        return sb.ToString();
    }
}