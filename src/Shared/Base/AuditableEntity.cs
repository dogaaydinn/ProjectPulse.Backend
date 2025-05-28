namespace Shared.Base;

public abstract class AuditableEntity : BaseAuditableEntity<Guid>
{
    protected AuditableEntity() : base(Guid.NewGuid()) { }
    protected AuditableEntity(Guid id) : base(id) { }
}