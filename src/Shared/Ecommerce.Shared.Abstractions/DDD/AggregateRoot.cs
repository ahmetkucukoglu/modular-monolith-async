namespace Ecommerce.Shared.Abstractions.DDD;

public abstract class AggregateRoot<TIdentity> : Entity<TIdentity> where TIdentity : EntityTypedId
{
    protected AggregateRoot(TIdentity id) : base(id)
    {
    }

    public AggregateRoot()
    {
        
    }
}