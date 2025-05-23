using Shared.Results;

namespace Shared.Exceptions;

public class ValidationException : AppException
{
    public IReadOnlyCollection<Error> Errors { get; }

    public ValidationException(IEnumerable<Error> errors)
        : base("VALIDATION_FAILED", "One or more validation errors occurred.")
    {
        var list = errors.ToList();
        Errors = list.AsReadOnly();
    }
}