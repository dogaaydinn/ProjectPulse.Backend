namespace Application.Common.Validation;

public class ValidationError
{
    public string PropertyName { get;  }
    public string ErrorMessage { get; } 

    public ValidationError(string propertyName, string errorMessage)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }
    public static ValidationError Create(string propertyName, string errorMessage)
    {
        return new ValidationError(propertyName, errorMessage);
    }
}