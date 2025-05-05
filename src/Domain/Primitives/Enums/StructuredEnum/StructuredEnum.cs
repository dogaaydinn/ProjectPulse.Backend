namespace Domain.Primitives.Enums.StructuredEnum;

public abstract class StructuredEnum<TEnum> : StructuredEnum<TEnum, int>
    where TEnum : StructuredEnum<TEnum, int>
{
    protected StructuredEnum(string name, int value)
        : base(name, value)
    {
    }
}