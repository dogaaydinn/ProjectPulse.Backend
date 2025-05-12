namespace Domain.Core.Primitives.Enums.Exceptions;

public class InvalidFlagEnumValueParseException : Exception
{
    public InvalidFlagEnumValueParseException(string message) : base(message) { }
}