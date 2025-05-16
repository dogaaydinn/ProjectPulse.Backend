namespace Shared.Exceptions;

public class BusinessException : AppException
{
    public BusinessException(string message, string? code = "Error.Business", Dictionary<string, object>? metadata = null)
        : base(code!, message, null, metadata)
    {
    }
}