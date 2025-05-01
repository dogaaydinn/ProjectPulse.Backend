namespace Shared.Base;

public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
{
    public DateTime? CreatedDate { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Guid? UpdatedBy { get; set; }
}