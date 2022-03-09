using Ecommerce.Shared.Abstractions.DDD;

namespace Ecommerce.Inventory.Core.ValueObjects;

public class ProductSku : ValueObject
{
    public string Sku { get; set; }

    public ProductSku(string sku)
    {
        Sku = sku;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Sku;
    }
}