namespace Shared.Exceptions;

public class AppException : Exception
{
    public string Code { get; }

    public AppException(string message, string code = "App.Exception") : base(message)
    {
        Code = code;
    }
}