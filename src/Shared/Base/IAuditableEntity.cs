namespace Shared.Base;

public interface IAuditableEntity
{
    DateTime? CreatedDate { get; set; }
    Guid? CreatedBy { get; set; }
    DateTime? UpdatedDate { get; set; }
    Guid? UpdatedBy { get; set; }
}