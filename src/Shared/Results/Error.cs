namespace Shared.Results;

public class Error
{
    public string Code { get; }
    public string Message { get; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public static Error None => new(string.Empty, string.Empty);
    public static Error NotFound(string entity) => new("NotFound", $"{entity} not found.");
    public static Error Validation(string message) => new("Validation", message);
}