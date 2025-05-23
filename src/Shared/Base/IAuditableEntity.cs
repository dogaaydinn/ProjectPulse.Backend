using Shared.Time;

namespace Shared.Base;

public interface IAuditableEntity<TId> : IEntity<TId>
    where TId : notnull
{
    DateTime CreatedDate { get; }
    Guid     CreatedBy   { get; }
    DateTime? UpdatedDate { get; }
    Guid?    UpdatedBy   { get; }
    bool     IsDeleted   { get; }
    DateTime? DeletedDate { get; }
    Guid?    DeletedBy   { get; }

    void TrackCreation(Guid userId, IClock clock);
    void TrackModification(Guid userId, IClock clock);
    void SoftDelete(Guid userId, IClock clock);
    void Restore(Guid userId, IClock clock);
}