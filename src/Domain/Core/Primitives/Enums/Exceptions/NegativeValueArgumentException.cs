namespace Domain.Core.Primitives.Enums.Exceptions;

public class NegativeValueArgumentException : Exception
{
    public NegativeValueArgumentException(string message) : base(message) { }
}