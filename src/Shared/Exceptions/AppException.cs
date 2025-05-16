using Shared.Results;

namespace Shared.Exceptions;

public class AppException(
    string code,
    string message,
    string? details = null,
    Dictionary<string, object>? metadata = null)
    : Exception(message)
{
    private string Code { get; } = code;
    private string? Details { get; } = details;
    private IReadOnlyDictionary<string, object> Metadata { get; } = metadata ?? new Dictionary<string, object>();

    public AppException(Error error)
        : this(error.Code, error.Message, null, new Dictionary<string, object>(error.Metadata)) { }

    protected AppException WithMetadata(string key, object value)
    {
        var updated = new Dictionary<string, object>(Metadata)
        {
            [key] = value
        };
        return new AppException(Code, Message, Details, updated);
    }
}