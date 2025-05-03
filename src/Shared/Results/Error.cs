using Shared.Constants;

namespace Shared.Results;

public class Error
{
    public string Code { get; }
    public string Message { get; }

    private Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
    
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
}