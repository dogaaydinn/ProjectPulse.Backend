using Shared.Constants;
using Shared.Results;

namespace Shared.Exceptions;

public abstract class ValidationException(List<Error> errors) : AppException("Validation failed.", ErrorCodes.Validation)
{
    public List<Error> Errors { get; } = errors;
}