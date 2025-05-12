namespace Domain.Core.Primitives.Enums.Exceptions;

public class StructuredEnumNotFoundException : Exception
{
    public StructuredEnumNotFoundException(string message) : base(message) { }
}