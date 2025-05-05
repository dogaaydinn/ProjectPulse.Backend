namespace Domain.Primitives.Enums.StructuredEnum;

internal static class ThrowHelper
{
    public static void ThrowArgumentNullException(string paramName)
        => throw new ArgumentNullException(paramName);

    public static void ThrowArgumentNullOrEmptyException(string paramName)
        => throw new ArgumentException("Argument cannot be null or empty.", paramName);
}