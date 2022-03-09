using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;

namespace Ecommerce.Shared.Bus;

public static class BusConfiguration
{
    private static Action<IServiceCollectionBusConfigurator> _busConfigurators;
    private static Action<IInMemoryBusFactoryConfigurator, IBusRegistrationContext> _busFactoryConfigurators;
    private static Dictionary<string, string> _queues = new ();
    
    public static void AddBusConfigurator(Action<IServiceCollectionBusConfigurator> busConfigurator)
    {
        _busConfigurators += busConfigurator;
    }

    internal static Action<IServiceCollectionBusConfigurator> GetBusConfigurators()
    {
        return _busConfigurators;
    }
    
    public static void AddBusFactoryConfigurator(Action<IInMemoryBusFactoryConfigurator, IBusRegistrationContext> busFactoryConfigurator)
    {
        _busFactoryConfigurators += busFactoryConfigurator;
    }

    internal static Action<IInMemoryBusFactoryConfigurator, IBusRegistrationContext> GetBusFactoryConfigurators()
    {
        return _busFactoryConfigurators;
    }
    
    public static void AddQueueName<TEvent>(string queue)
    {
        _queues.Add(typeof(TEvent).Name, queue);
    }

    public static string GetQueueName<TEvent>()
    {
        return "queue:" + _queues[typeof(TEvent).Name];
    }
}