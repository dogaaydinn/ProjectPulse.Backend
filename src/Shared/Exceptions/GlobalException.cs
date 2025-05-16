namespace Shared.Exceptions;

public class GlobalException : AppException
{
    public GlobalException(string message, Exception? inner = null)
        : base("Error.Global", message)
    {
        if (inner is not null)
        {
            this.WithMetadata("InnerException", inner.Message);
        }
    }
}