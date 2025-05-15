using System.Reflection;
using Domain.Core.Primitives.Enums.Helpers;
using Domain.Core.Primitives.Enums.Interfaces;

namespace Domain.Core.Primitives.Enums.Base;

public abstract class StructuredEnum<TEnum, TValue> : IStructuredEnum
    where TEnum : StructuredEnum<TEnum, TValue>
    where TValue : IComparable<TValue>, IEquatable<TValue>
{
    private static readonly Lazy<IReadOnlyList<TEnum>> _allValues = new(() => 
        typeof(TEnum)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(TEnum))
            .Select(f => (TEnum)f.GetValue(null)!)
            .ToList()
            .AsReadOnly());

    protected StructuredEnum(string name, TValue value)
    {
        if (string.IsNullOrWhiteSpace(name))
            ThrowHelper.ThrowArgumentNullOrEmptyException(nameof(name));
        if (value is null)
            ThrowHelper.ThrowArgumentNullException(nameof(value));

        Name = name;
        Value = value;
    }

    public string Name { get; }
    public TValue Value { get; init; }
    private static IReadOnlyList<TEnum> AllValues => _allValues.Value;

    public override string ToString() => Name;

    public override bool Equals(object? obj)
        => obj is StructuredEnum<TEnum, TValue> other &&
           EqualityComparer<TValue>.Default.Equals(Value, other.Value);

    public override int GetHashCode() => Value!.GetHashCode();

    public static bool operator ==(StructuredEnum<TEnum, TValue> left, StructuredEnum<TEnum, TValue> right)
        => left?.Equals(right) ?? false;

    public static bool operator !=(StructuredEnum<TEnum, TValue> left, StructuredEnum<TEnum, TValue> right)
        => !(left == right);

    public static TEnum FromName(string name)
        => AllValues.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
        ?? throw new ArgumentException($"No {typeof(TEnum).Name} with name '{name}' found.");

    public static TEnum FromValue(TValue value)
        => AllValues.FirstOrDefault(x => x.Value.Equals(value))
        ?? throw new ArgumentException($"No {typeof(TEnum).Name} with value '{value}' found.");

    public static bool TryFromName(string name, out TEnum? result)
    {
        result = AllValues.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return result != null;
    }

    public static bool TryFromValue(TValue value, out TEnum? result)
    {
        result = AllValues.FirstOrDefault(x => x.Value.Equals(value));
        return result != null;
    }
    
    public static bool IsValidValue(TValue value)
        => AllValues.Any(x => x.Value.Equals(value));
    
    public static bool IsValidName(string name)
        => AllValues.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
} 

