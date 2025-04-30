namespace Shared.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException(string resource, object key)
        : base($"{resource} with key '{key}' was not found.", "Error.NotFound") { }
}