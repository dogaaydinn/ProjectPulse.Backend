namespace Shared.Exceptions;

public class NotFoundException(string resource, object key)
    : AppException($"{resource} with key '{key}' was not found.", "Error.NotFound");