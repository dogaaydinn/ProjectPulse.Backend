// Shared/Base/AuditableEntityNonGeneric.cs (örnek dosya adı)
namespace Shared.Base;

public abstract class AuditableEntity : AuditableEntity<Guid>
{
    protected AuditableEntity() : base(Guid.NewGuid()) { }
    protected AuditableEntity(Guid id) : base(id) { }
}