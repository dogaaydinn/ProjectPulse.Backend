namespace Shared.Base;

public interface IEntity<TId>
{
    TId Id { get; }
    long Version { get; }
}