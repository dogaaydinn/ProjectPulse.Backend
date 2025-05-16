namespace Shared.Base;

public abstract class Entity<TId> : IEntity<TId> where TId : notnull
{
    public TId Id { get; protected set; }
    public int Version { get; protected set; }

    protected Entity(TId id)
    {
        if (id is null) throw new ArgumentNullException(nameof(id));
        Id = id;
    }
}