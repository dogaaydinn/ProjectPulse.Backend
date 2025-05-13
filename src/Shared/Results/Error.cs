using Shared.Constants;

namespace Shared.Results;

public class Error(string code, string message)
{
    public string Code { get; } = code;
    public string Message { get; } = message;

    public static Error NotFound(string entityName, Guid id)
    {
        return new Error(
            ErrorCodes.NotFound,
            $"{entityName} with ID '{id}' was not found."
        );
    }
    
    public static Error Validation(string message)
    {
        return new Error(ErrorCodes.Validation, message);
    }
    
    public static Error Unexpected(string message)
    {
        return new Error(ErrorCodes.Unexpected, message);
    }
    
    public static Error NotFound(string message)
    {
        return new Error(ErrorCodes.NotFound, message);
    }
    
    public static Error Schedule(string message)
    {
        return new Error(ErrorCodes.Schedule, message);
    }
}