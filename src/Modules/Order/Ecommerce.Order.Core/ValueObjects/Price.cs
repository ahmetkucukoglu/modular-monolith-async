using Ecommerce.Shared.Abstractions.DDD;

namespace Ecommerce.Order.Core.ValueObjects;

public class Price : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }

    protected Price()
    {
        
    }

    public Price(decimal amount, string? currency)
    {
        Amount = amount;
        Currency = currency ?? "TL";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}