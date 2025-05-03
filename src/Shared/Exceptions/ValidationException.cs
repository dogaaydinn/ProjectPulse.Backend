using Shared.Constants;
using Shared.Results;

namespace Shared.Exceptions;

public class ValidationException : AppException
{
    public List<Error> Errors { get; }

    public ValidationException(List<Error> errors)
        : base("Validation failed.", ErrorCodes.Validation)
    {
        Errors = errors;
    }
}