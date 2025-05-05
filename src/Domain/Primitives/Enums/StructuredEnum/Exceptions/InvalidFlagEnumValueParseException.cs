namespace Domain.Primitives.Enums.StructuredEnum.Exceptions;

public class InvalidFlagEnumValueParseException : Exception
{
    public InvalidFlagEnumValueParseException(string message) : base(message) { }
}