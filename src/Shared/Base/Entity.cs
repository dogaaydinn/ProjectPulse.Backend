namespace Shared.Base;

public abstract class Entity : Entity<Guid>
{
    protected Entity(Guid id) : base(id) { }
}