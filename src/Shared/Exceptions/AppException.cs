namespace Shared.Exceptions;

public class AppException(string message, string code = "App.Exception") : Exception(message)
{
    public string Code { get; } = code;
}