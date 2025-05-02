namespace Application.Common.Validation;

public class ValidationResult
{
    public bool IsValid => Errors.Count == 0;
    public List<string> Errors { get; } = new();

    public void AddError(string message)
    {
        if (!string.IsNullOrWhiteSpace(message))
            Errors.Add(message);
    }

    public static ValidationResult Success() => new();
}