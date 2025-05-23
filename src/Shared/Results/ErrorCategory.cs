namespace Shared.Results;

public enum ErrorCategory
{
    Validation,
    Domain,
    Infrastructure,
    Security,
    NotFound,
    Unauthorized,
    Forbidden,
    Conflict,
    Timeout,
    RateLimited,
    Unexpected,
    Localization,
    External
}