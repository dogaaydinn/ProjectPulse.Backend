namespace Shared.Base;

public interface IAuditableEntity
{
    DateTime CreatedDate { get; }
    Guid CreatedBy { get; }
    DateTime? UpdatedDate { get; }
    Guid? UpdatedBy { get; }

    void TrackCreation(Guid userId);
    void TrackModification(Guid userId);
}