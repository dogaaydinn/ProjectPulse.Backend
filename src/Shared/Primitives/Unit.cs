namespace Shared.Primitives;
public readonly record struct Unit
{
    public static readonly Unit Value = default;
    public static readonly Task<Unit> Task = System.Threading.Tasks.Task.FromResult(Value);
}