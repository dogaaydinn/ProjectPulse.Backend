using Shared.Results;

namespace Shared.Exceptions;

public class ValidationException : AppException
{
    private IReadOnlyCollection<Error> Errors { get; }

    public ValidationException(IEnumerable<Error> errors)
        : base("Error.Validation", "One or more validation errors occurred.")
    {
        Errors = errors.ToList().AsReadOnly();
        this.WithMetadata("ErrorCount", Errors.Count);
    }

    private new ValidationException WithMetadata(string key, object value)
    {
        base.WithMetadata(key, value);
        return this;
    }
}