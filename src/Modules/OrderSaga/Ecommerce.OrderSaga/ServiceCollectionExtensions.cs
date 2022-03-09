using Ecommerce.Inventory.Shared.Events;
using Ecommerce.Order.Shared.Events;
using Ecommerce.Payment.Shared.Events;
using Ecommerce.Shared.Bus;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.OrderSaga;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrderSaga(this IServiceCollection serviceCollection)
    {
        BusConfiguration.AddQueueName<OrderReceived>("order-saga");
        BusConfiguration.AddQueueName<StockReserved>("order-saga");
        BusConfiguration.AddQueueName<OutOfStock>("order-saga");
        BusConfiguration.AddQueueName<PaymentMade>("order-saga");
        BusConfiguration.AddQueueName<PaymentFailed>("order-saga");
        
        BusConfiguration.AddBusConfigurator(busConfigurator =>
        {
            busConfigurator.AddSagaStateMachine<OrderSagaStateMachine, OrderStateMachineInstance>().InMemoryRepository();
        });
        
        BusConfiguration.AddBusFactoryConfigurator((busFactoryConfigurator, registration) =>
        {
            busFactoryConfigurator.ReceiveEndpoint("order-saga", e => e.StateMachineSaga<OrderStateMachineInstance>(registration));
        });

        return serviceCollection;
    }
}