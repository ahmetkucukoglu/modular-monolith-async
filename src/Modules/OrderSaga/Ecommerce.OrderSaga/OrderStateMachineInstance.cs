using Automatonymous;

namespace Ecommerce.OrderSaga;

public class OrderStateMachineInstance : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public Guid OrderId { get; set; }
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; }
    public List<(string Sku, int Quantity)> Products { get; set; }
}