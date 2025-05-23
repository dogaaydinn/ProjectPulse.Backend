namespace Shared.Abstractions.Caching;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class CacheResponseAttribute : Attribute
{
    public int Duration { get; }

    public CacheResponseAttribute(int duration)
    {
        Duration = duration;
    }
}