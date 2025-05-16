using Shared.Constants;

namespace Shared.Results;

public static class ErrorFactory
{
    private static ErrorBuilder Create(string code) => new(code);

    public static Error Required(string field) =>
        Create(ErrorCodes.Validation)
            .WithMessage($"{field} is required.")
            .WithMetadata("Field", field)
            .Build();

    public static Error InvalidFormat(string field) =>
        Create(ErrorCodes.Validation)
            .WithMessage($"{field} has an invalid format.")
            .WithMetadata("Field", field)
            .Build();

    public static Error OutOfRange(string field, string range) =>
        Create(ErrorCodes.Validation)
            .WithMessage($"{field} must be within {range}.")
            .WithMetadata("Field", field)
            .WithMetadata("Range", range)
            .Build();

    public static Error EnumInvalid(string field, IEnumerable<string> validOptions) =>
        Create(ErrorCodes.Validation)
            .WithMessage($"Invalid {field} value. Must be one of: {string.Join(", ", validOptions)}")
            .WithMetadata("Field", field)
            .WithMetadata("ValidOptions", validOptions)
            .Build();

    public static Error Unexpected(string message, Exception? ex = null)
    {
        var error = Create(ErrorCodes.Unexpected)
            .WithMessage(message);

        if (ex != null)
        {
            error.WithMetadata("ExceptionType", ex.GetType().Name);
        }

        return error.Build();
    }

    public static Error NotFound(string entity, object id) =>
        Create(ErrorCodes.NotFound)
            .WithMessage($"{entity} with ID '{id}' was not found.")
            .WithMetadata("Entity", entity)
            .WithMetadata("Id", id)
            .Build();

    public static Error Custom(string code, string message) =>
        Create(code)
            .WithMessage(message)
            .Build();
}

public sealed class ErrorBuilder(string code)
{
    private string _message = string.Empty;
    private readonly Dictionary<string, object> _metadata = new();

    public ErrorBuilder WithMessage(string message)
    {
        _message = message;
        return this;
    }

    public ErrorBuilder WithMetadata(string key, object value)
    {
        _metadata[key] = value;
        return this;
    }

    public Error Build()
    {
        var error = new Error(code, _message);
        foreach (var kv in _metadata)
        {
            error.WithMetadata(kv.Key, kv.Value);
        }
        return error;
    }
    
}