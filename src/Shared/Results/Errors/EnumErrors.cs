namespace Shared.Results.Errors;

public static class EnumErrors
{
    public static string Required(string field) => $"{field} is required.";

    public static string Invalid(string field, IEnumerable<string> validOptions) =>
        $"Invalid {field}. Must be one of: {string.Join(", ", validOptions)}.";
}
