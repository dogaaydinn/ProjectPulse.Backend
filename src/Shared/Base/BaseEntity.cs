namespace Shared.Base;

public abstract class BaseEntity<TId> : IEntity<TId>, IEquatable<BaseEntity<TId>>
    where TId : notnull
{
    public TId Id { get; protected set; }
    public long Version { get; protected set; }

    protected BaseEntity(TId id)
    {
        if (id is null) throw new ArgumentNullException(nameof(id));
        Id = id;
    }

    public override bool Equals(object? obj) =>
        obj is BaseEntity<TId> other && Equals(other);

    public bool Equals(BaseEntity<TId>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;
        if (IsTransient() || other.IsTransient()) return false;
        return Id.Equals(other.Id);
    }

    public override int GetHashCode() =>
        Id.GetHashCode() ^ 31;

    public static bool operator ==(BaseEntity<TId>? left, BaseEntity<TId>? right) =>
        left?.Equals(right) ?? right is null;

    public static bool operator !=(BaseEntity<TId>? left, BaseEntity<TId>? right) =>
        !(left == right);

    private bool IsTransient()
    {
        if (Id is Guid g) return g == Guid.Empty;
        if (Id is int i) return i == default;
        return Id.Equals(default(TId));
    }
}