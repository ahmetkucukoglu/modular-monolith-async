using Ecommerce.Shared.Bus;
using Ecommerce.Shared.CQRS;
using Ecommerce.Shared.DDD;

namespace Ecommerce.Bootstrapper;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBootstrapper(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddControllers().ConfigureApplicationPartManager(manager =>
        {
            manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
        });

        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName is not null && s.FullName.StartsWith("Ecommerce"))
            .ToArray();

        serviceCollection
            .AddCQRS(assemblies)
            .AddDDD(assemblies)
            .AddBus();

        return serviceCollection;
    }
}