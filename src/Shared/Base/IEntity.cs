namespace Shared.Base;

public interface IEntity<out TId>
{
    TId Id { get; }
    int Version { get; }
}