using System.Text;
using Shared.Constants;
using Shared.Validation;

namespace Shared.Results;

public sealed record Error
{
    public string Code { get; }
    public string Message { get; }
    public IReadOnlyDictionary<string, object> Metadata { get; }
    public Exception? Exception { get; private init; }

    public Error(string code, string message, Dictionary<string, object>? metadata = null)
    {
        Code = Guard.AgainstNullOrEmpty(code, nameof(code));
        Message = Guard.AgainstNullOrEmpty(message, nameof(message));
        Metadata = metadata?.AsReadOnly() ?? new Dictionary<string, object>().AsReadOnly();
    }

    // Fluent API
    public Error WithMetadata(string key, object value)
    {
        var newMetadata = new Dictionary<string, object>(Metadata)
        {
            [key] = value
        };
        return this with { Metadata = newMetadata.AsReadOnly() };
    }

    private Error WithException(Exception ex) => 
        this with { Exception = ex };

    // Factory Methods
    public static Error Null() => 
        new Error(ErrorCodes.NullValue, "Null reference encountered");

    public static Error Unexpected(string message) => 
        new Error(ErrorCodes.Unexpected, message);

    public static Error Unexpected(Exception ex) => 
        new Error(ErrorCodes.Unexpected, ex.Message)
            .WithException(ex)
            .WithMetadata("ExceptionType", ex.GetType().Name)
            .WithMetadata("StackTrace", ex.StackTrace ?? string.Empty);

    public static Error Validation(string message, string? code = null) => 
        new Error(code ?? ErrorCodes.Validation, message);

    public static Error NotFound(string resource, object id) => 
        new Error(ErrorCodes.NotFound, $"{resource} with ID '{id}' was not found")
            .WithMetadata("Resource", resource)
            .WithMetadata("Id", id);

    public static Error Conflict(string resource) => 
        new Error(ErrorCodes.Conflict, message: $"{resource} already exists")
            .WithMetadata("Resource", resource);

    // Pattern Matching Support
    public void Deconstruct(out string code, out string message, out IReadOnlyDictionary<string, object> metadata)
    {
        code = Code;
        message = Message;
        metadata = Metadata;
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder($"[{Code}] {Message}");

        if (Metadata.Count > 0)
        {
            sb.Append(" | Metadata: { ");
            sb.Append(string.Join(", ", Metadata.Select(kv => $"{kv.Key}: {kv.Value}")));
            sb.Append(" }");
        }

        if (Exception != null)
        {
            sb.Append($" | Exception: {Exception.GetType().Name}");
        }

        return sb.ToString();
    }
}