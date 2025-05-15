namespace Shared.Base;

public abstract class Entity<TId> : IEntity<TId> where TId : notnull
{
    public TId Id { get; protected set; } = default!;
    public int Version { get; protected set; }

    protected Entity(TId id) => Id = id;
    
}

public abstract class Entity : Entity<Guid>, IEntity
{
    protected Entity() : base(Guid.NewGuid()) { }
}