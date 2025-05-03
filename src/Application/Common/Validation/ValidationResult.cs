namespace Application.Common.Validation;

public class ValidationResult
{
    public bool IsValid => Errors.Count == 0;
    private List<ValidationError> Errors { get; } = [];

    public void AddError(string propertyName, string message)
    {
        if (!string.IsNullOrWhiteSpace(message))
            Errors.Add(ValidationError.Create(propertyName, message));
    }

    public static ValidationResult Success() => new();
}