using Shared.Results;
using Shared.Results.Errors.ValueObjects;

namespace Shared.Exceptions;

public class LocalizationException : AppException
{
    public LocalizationException(
        IErrorFactory errors,
        string cultureCode)
        : base(errors.MissingCulture(cultureCode))
    {
    }
}