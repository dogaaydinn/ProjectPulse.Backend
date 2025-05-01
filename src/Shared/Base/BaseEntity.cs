namespace Shared.Base;

public abstract class BaseEntity : IEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
}