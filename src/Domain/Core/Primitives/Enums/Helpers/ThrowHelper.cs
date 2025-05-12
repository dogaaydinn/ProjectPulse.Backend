namespace Domain.Core.Primitives.Enums.Helpers;
internal static class ThrowHelper
{
    public const string InvalidEnumNameMessage = "Enum name cannot be null or empty.";
    public const string InvalidEnumValueMessage = "Invalid enum value. Must be one of: {0}";

    public static void ThrowArgumentNullException(string paramName)
        => throw new ArgumentNullException(paramName);

    public static void ThrowArgumentNullOrEmptyException(string paramName)
        => throw new ArgumentException("Argument cannot be null or empty.", paramName);
}