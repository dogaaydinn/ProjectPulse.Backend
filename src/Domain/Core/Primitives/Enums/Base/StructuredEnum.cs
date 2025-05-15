namespace Domain.Core.Primitives.Enums.Base;

public abstract class StructuredEnum<TEnum>(string name, int value) : StructuredEnum<TEnum, int>(name, value)
    where TEnum : StructuredEnum<TEnum, int>;