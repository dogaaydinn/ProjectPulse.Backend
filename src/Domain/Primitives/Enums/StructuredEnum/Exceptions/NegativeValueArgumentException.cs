namespace Domain.Primitives.Enums.StructuredEnum.Exceptions;

public class NegativeValueArgumentException : Exception
{
    public NegativeValueArgumentException(string message) : base(message) { }
}