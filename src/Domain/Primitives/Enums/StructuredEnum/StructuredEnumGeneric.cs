using System.Reflection;

namespace Domain.Primitives.Enums.StructuredEnum;

public abstract class StructuredEnum<TEnum, TValue> : IStructuredEnum
    where TEnum : StructuredEnum<TEnum, TValue>
    where TValue : IComparable<TValue>, IEquatable<TValue>
{
    private static readonly Lazy<List<TEnum>> Options =
        new(() => typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(f => f.FieldType == typeof(TEnum))
            .Select(f => (TEnum)f.GetValue(null)!)
            .ToList());

    protected StructuredEnum(string name, TValue value)
    {
        if (string.IsNullOrWhiteSpace(name))
            ThrowHelper.ThrowArgumentNullOrEmptyException(nameof(name));
        if (value == null)
            ThrowHelper.ThrowArgumentNullException(nameof(value));

        Name = name;
        Value = value;
    }

    public string Name { get; }

    public  TValue Value { get; init; }

    public static IReadOnlyCollection<TEnum> List => Options.Value;

    public override string ToString() => Name;

    public override bool Equals(object? obj)
        => obj is StructuredEnum<TEnum, TValue> other && EqualityComparer<TValue>.Default.Equals(Value, other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(StructuredEnum<TEnum, TValue> left, StructuredEnum<TEnum, TValue> right)
        => left?.Equals(right) ?? false;

    public static bool operator !=(StructuredEnum<TEnum, TValue> left, StructuredEnum<TEnum, TValue> right)
        => !(left == right);

    public static TEnum FromValue(TValue value)
        => Options.Value.FirstOrDefault(x => x.Value.Equals(value))
            ?? throw new ArgumentException($"No {typeof(TEnum).Name} with value '{value}' found.");

    public static TEnum FromName(string name)
        => Options.Value.FirstOrDefault(x => x.Name == name)
            ?? throw new ArgumentException($"No {typeof(TEnum).Name} with name '{name}' found.");
}
