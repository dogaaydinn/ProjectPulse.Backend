using Shared.Results;

namespace Shared.Exceptions;

public class LocalizationException : AppException
{
    public LocalizationException(string cultureCode)
        : base(ErrorFactory.LocalizedString.MissingCulture(cultureCode))
    {
    }
}