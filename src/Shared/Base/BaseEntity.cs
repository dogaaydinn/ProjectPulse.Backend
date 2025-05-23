namespace Shared.Base;

public abstract class Entity<TId> : IEntity<TId>, IEquatable<Entity<TId>> where TId : notnull
{
    public TId Id { get; protected set; }
    public long Version { get; protected set; }

    protected Entity(TId id)
    {
        if (id is null) throw new ArgumentNullException(nameof(id));
        Id = id;
    }

    public override bool Equals(object? obj) => obj is Entity<TId> e && Equals(e);
    public bool Equals(Entity<TId>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;
        if (IsTransient() || other.IsTransient()) return false;
        return Id.Equals(other.Id);
    }

    public override int GetHashCode() => Id.GetHashCode() ^ 31;

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right) =>
        left?.Equals(right) ?? right is null;

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) =>
        !(left == right);

    private bool IsTransient()
    {
        if (Id is Guid g) return g == Guid.Empty;
        if (Id is int i) return i == default;
        return Id.Equals(default(TId));
    }
}