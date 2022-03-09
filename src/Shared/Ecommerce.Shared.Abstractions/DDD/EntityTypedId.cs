namespace Ecommerce.Shared.Abstractions.DDD;

public abstract class EntityTypedId : ValueObject
{
    public Guid Value { get; }

    public EntityTypedId() => Value = Guid.NewGuid();
    public EntityTypedId(Guid value) => Value = value;

    public override string ToString() => Value.ToString();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public static implicit operator Guid(EntityTypedId id) => id.Value;
}