namespace Shared.Base;

public abstract class AggregateRoot : AggregateRoot<Guid>
{
    protected AggregateRoot(Guid id) : base(id) { }
}