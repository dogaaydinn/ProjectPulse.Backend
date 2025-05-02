using System.ComponentModel.DataAnnotations;

namespace Application.Common.Validation;

public interface IValidator<T>
{
    ValidationResult Validate(T request);
}