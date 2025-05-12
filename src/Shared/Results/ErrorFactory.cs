using Shared.Constants;
using Shared.Results.Errors;

namespace Shared.Results;

public static class ErrorFactory
{
    public static Error EnumRequired(string field)
        => new(ErrorCodes.Validation, EnumErrors.Required(field));

    public static Error EnumInvalid(string field, IEnumerable<string> validOptions)
        => new(ErrorCodes.Validation, EnumErrors.Invalid(field, validOptions));

    public static Error Custom(string code, string message)
        => new(code, message);
    public static Error EnumInvalidValue(string field, IEnumerable<int> validValues)
        => new(ErrorCodes.Validation, 
            $"Invalid {field} value. Must be one of: {string.Join(", ", validValues)}");

    public static Error EnumOutOfRange(string field, int min, int max)
        => new(ErrorCodes.Validation, 
            $"{field} must be between {min} and {max} (inclusive).");
}