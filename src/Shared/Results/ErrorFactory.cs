using Shared.Constants;
using Shared.Results.Errors;

namespace Shared.Results;

public static class ErrorFactory
{
    public static Error Validation(string code, string message) =>
        new(code, message);

    public static Error Required(string fieldName) =>
        new(ErrorCodes.Validation, $"{fieldName} is required.");

    public static Error InvalidFormat(string fieldName) =>
        new(ErrorCodes.Validation, $"{fieldName} has an invalid format.");

    public static Error OutOfRange(string fieldName, string range) =>
        new(ErrorCodes.Validation, $"{fieldName} must be within {range}.");

    public static Error EnumRequired(string field) =>
        new(ErrorCodes.Validation, EnumErrors.Required(field));

    public static Error EnumInvalid(string field, IEnumerable<string> validOptions) =>
        new(ErrorCodes.Validation, EnumErrors.Invalid(field, validOptions));

    public static Error EnumInvalidValue(string field, IEnumerable<int> validValues) =>
        new(ErrorCodes.Validation, 
            $"Invalid {field} value. Must be one of: {string.Join(", ", validValues)}");

    public static Error EnumOutOfRange(string field, int min, int max) =>
        new(ErrorCodes.Validation, 
            $"{field} must be between {min} and {max} (inclusive).");
    
    public static Error Unexpected(string message) =>
        new(ErrorCodes.Unexpected, message);

    public static Error Custom(string code, string message) =>
        new(code, message);

    public static Error NotFound(string entityName, Guid id) =>
        new(ErrorCodes.NotFound, $"{entityName} with ID '{id}' was not found.");
}