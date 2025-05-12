using Shared.Exceptions;

namespace Domain.Core.Helper;

public static class BaseValidationHelper
{
    public static void ThrowIfNullOrEmpty(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new AppException($"Validation.{fieldName}", $"{fieldName} is required.");
    }

    public static void ThrowIfInvalidGuid(Guid id, string fieldName)
    {
        if (id == Guid.Empty)
            throw new AppException($"Validation.{fieldName}", $"{fieldName} must be a valid GUID.");
    }

    public static void ThrowIfPastDate(DateTime date, string fieldName)
    {
        if (date < DateTime.UtcNow)
            throw new AppException($"Validation.{fieldName}", $"{fieldName} must be in the future.");
    }
}