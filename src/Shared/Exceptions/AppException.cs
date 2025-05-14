using Shared.Results;

namespace Shared.Exceptions;

public class AppException : Exception
{
    public string Code { get; }

    public AppException(Error error)
        : base(error.Message)
    {
        Code = error.Code;
    }

    public AppException(string code, string message)
        : base(message)
    {
        Code = code;
    }
}