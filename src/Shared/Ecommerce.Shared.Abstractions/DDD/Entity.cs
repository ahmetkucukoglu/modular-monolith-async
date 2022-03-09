namespace Ecommerce.Shared.Abstractions.DDD;

public abstract class Entity
{
    private readonly List<IDomainEvent> _events = new();
    
    protected void RaiseEvent(IDomainEvent @event) => _events.Add(@event);

    public List<IDomainEvent> GetEvents() => _events;

    public void ClearEvents() => _events.Clear();
}


public abstract class Entity<TIdentity> : Entity, IEquatable<Entity<TIdentity>> where TIdentity : EntityTypedId
{
    public TIdentity Id { get; protected set; }

    protected Entity(TIdentity id) => Id = id;

    protected Entity() { }

    public bool Equals(Entity<TIdentity>? other)
    {
        if (ReferenceEquals(null, other)) return false;

        if (ReferenceEquals(this, other)) return true;

        return Equals(Id, other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;

        if (ReferenceEquals(this, obj)) return true;

        if (obj.GetType() != GetType()) return false;

        return Equals((Entity<TIdentity>)obj);
    }

    public override int GetHashCode() => GetType().GetHashCode() * 907 + Id.GetHashCode();
    
    public override string ToString() => $"{GetType().Name}#[Identity={Id}]";
    
    public static bool operator ==(Entity<TIdentity>? a, Entity<TIdentity>? b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity<TIdentity> a, Entity<TIdentity> b)
    {
        return !(a == b);
    }
}