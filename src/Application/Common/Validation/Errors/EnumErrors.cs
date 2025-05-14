namespace Application.Common.Validation.Errors;

public static class EnumErrors
{
    public static string Required(string field) => $"{field} is required.";
    public static string Invalid(string field, IEnumerable<string> options)
        => $"Invalid {field}. Allowed values: {string.Join(", ", options)}";
}