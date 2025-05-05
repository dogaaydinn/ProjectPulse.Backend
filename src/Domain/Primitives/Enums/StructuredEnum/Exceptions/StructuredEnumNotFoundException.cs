namespace Domain.Primitives.Enums.StructuredEnum.Exceptions;

public class StructuredEnumNotFoundException : Exception
{
    public StructuredEnumNotFoundException(string message) : base(message) { }
}