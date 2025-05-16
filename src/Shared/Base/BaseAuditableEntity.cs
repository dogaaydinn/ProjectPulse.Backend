namespace Shared.Base;

public abstract class AuditableEntity<TId>(TId id) : Entity<TId>(id), IAuditableEntity
    where TId : notnull
{
    public DateTime CreatedDate { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTime? UpdatedDate { get; private set; }
    public Guid? UpdatedBy { get; private set; }

    public void TrackCreation(Guid userId)
    {
        CreatedDate = DateTime.UtcNow;
        CreatedBy = userId;
        Version++;
    }

    public void TrackModification(Guid userId)
    {
        UpdatedDate = DateTime.UtcNow;
        UpdatedBy = userId;
        Version++;
    }
}