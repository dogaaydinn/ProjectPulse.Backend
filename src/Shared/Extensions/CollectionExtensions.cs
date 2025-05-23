namespace Shared.Extensions;

public static class CollectionExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
        => source == null || !source.Any();

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);
        foreach (var item in source)
            action(item);
    }

    public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> source)
        => source?.ToList().AsReadOnly() 
           ?? throw new ArgumentNullException(nameof(source));

    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? source)
        => source ?? [];
}