namespace Application.Common.Validation;

public interface IValidator<in T>
{
    ValidationResult Validate(T request);
}