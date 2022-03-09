using Automatonymous;
using Ecommerce.Inventory.Shared.Events;
using Ecommerce.Order.Shared.Events;
using Ecommerce.Payment.Shared.Events;
using Ecommerce.Shared.Bus;
using Microsoft.Extensions.Logging;

namespace Ecommerce.OrderSaga;

public class OrderSagaStateMachine : MassTransitStateMachine<OrderStateMachineInstance>
{
    private Event<OrderReceived> OrderReceivedEvent { get; set; }
    private Event<StockReserved> StockReservedEvent { get; set; }
    private Event<OutOfStock> OutOfStockEvent { get; set; }
    private Event<PaymentMade> PaymentMadeEvent { get; set; }
    private Event<PaymentFailed> PaymentFailedEvent { get; set; }

    private State OrderReceived { get; set; }
    private State StockReserved { get; set; }
    private State OutOfStock { get; set; }
    private State PaymentMade { get; set; }
    private State PaymentFailed { get; set; }


    public OrderSagaStateMachine(ILogger<OrderSagaStateMachine> logger)
    {
        InstanceState(instance => instance.CurrentState);

        Event(() => OrderReceivedEvent,
            configurator =>
                configurator.CorrelateBy<Guid>(
                        p => p.CorrelationId,
                        e => e.Message.CorrelationId)
                    .SelectId(e => e.Message.CorrelationId));

        Event(() => StockReservedEvent,
            configurator =>
                configurator.CorrelateById(e => e.Message.CorrelationId));

        Event(() => OutOfStockEvent,
            configurator =>
                configurator.CorrelateById(e => e.Message.CorrelationId));

        Event(() => PaymentMadeEvent,
            configurator =>
                configurator.CorrelateById(e => e.Message.CorrelationId));

        Event(() => PaymentFailedEvent,
            configurator =>
                configurator.CorrelateById(e => e.Message.CorrelationId));

        Initially(When(OrderReceivedEvent)
            .Then(context =>
            {
                logger.LogInformation($"OrderReceived '{context.Data.OrderId}'");

                context.Instance.OrderId = context.Data.OrderId;
                context.Instance.TotalPrice = context.Data.TotalPrice;
                context.Instance.Currency = context.Data.Currency;
                context.Instance.Products = context.Data.Products;
            })
            .TransitionTo(OrderReceived)
            .Send(
                new Uri(BusConfiguration.GetQueueName<ReserveStock>()),
                context => new ReserveStock(context.Instance.OrderId,
                    context.Data.Products.Select(p => (p.Sku, p.Quantity)))));

        During(OrderReceived,
            When(StockReservedEvent)
                .Then(context => { logger.LogInformation($"StockReserved '{context.Data.OrderId}'"); })
                .TransitionTo(StockReserved)
                .Send(
                    new Uri(BusConfiguration.GetQueueName<MakePayment>()),
                    context => new MakePayment(context.Instance.OrderId, context.Instance.TotalPrice, context.Instance.Currency)),
            When(OutOfStockEvent)
                .Then(context => { logger.LogInformation($"OutOfStock '{context.Data.OrderId}'"); })
                .TransitionTo(OutOfStock)
                .Send(
                    new Uri(BusConfiguration.GetQueueName<OrderFailed>()),
                    context => new OrderFailed(context.Instance.OrderId, context.Data.Reason)));

        During(StockReserved,
            When(PaymentMadeEvent)
                .Then(context => { logger.LogInformation($"PaymentMade '{context.Data.OrderId}'"); })
                .TransitionTo(PaymentMade)
                .Send(
                    new Uri(BusConfiguration.GetQueueName<OrderPaid>()),
                    context => new OrderPaid(context.Instance.OrderId))
                .Finalize(),
            When(PaymentFailedEvent)
                .Then(context => { logger.LogInformation($"PaymentFailed '{context.Data.OrderId}'"); })
                .TransitionTo(PaymentFailed)
                .Send(
                    new Uri(BusConfiguration.GetQueueName<ReleaseStock>()),
                    context => new ReleaseStock(context.Instance.OrderId, context.Instance.Products))
                .Send(
                    new Uri(BusConfiguration.GetQueueName<OrderFailed>()),
                    context => new OrderFailed(context.Instance.OrderId, context.Data.Reason))
                .Finalize());

        SetCompletedWhenFinalized();
    }
}