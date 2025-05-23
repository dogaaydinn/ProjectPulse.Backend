using Shared.Time;

namespace Shared.Base;

public abstract class AuditableEntity<TId> : Entity<TId>, IAuditableEntity<TId>
    where TId : notnull
{
    public DateTime CreatedDate   { get; private set; }
    public Guid     CreatedBy     { get; private set; }
    public DateTime? UpdatedDate  { get; private set; }
    public Guid?    UpdatedBy     { get; private set; }
    public bool     IsDeleted     { get; private set; }
    public DateTime? DeletedDate  { get; private set; }
    public Guid?    DeletedBy     { get; private set; }

    protected AuditableEntity(TId id) : base(id) { }
    // Entity sınıfı parametresiz constructor'a sahip değil, bu yüzden default bir ID sağlamamız gerekiyor
    protected AuditableEntity() : base(default!) { }

    public void TrackCreation(Guid userId, IClock clock)
    {
        CreatedDate = clock.UtcNow;
        CreatedBy   = userId;
        Version++;
    }

    public void TrackModification(Guid userId, IClock clock)
    {
        UpdatedDate = clock.UtcNow;
        UpdatedBy   = userId;
        Version++;
    }

    public virtual void SoftDelete(Guid userId, IClock clock)
    {
        if (IsDeleted) return;
        IsDeleted   = true;
        DeletedDate = clock.UtcNow;
        DeletedBy   = userId;
        Version++;
    }

    public virtual void Restore(Guid userId, IClock clock)
    {
        if (!IsDeleted) return;
        IsDeleted   = false;
        DeletedDate = null;
        DeletedBy   = null;
        TrackModification(userId, clock);
    }
}