namespace Shared.Base;

public abstract class Entity : BaseEntity<Guid>
{
    protected Entity(Guid id) : base(id) { }
}