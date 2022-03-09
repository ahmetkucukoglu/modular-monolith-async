using Ecommerce.Payment.Core.Repositories;
using Ecommerce.Payment.Core.Services;
using Ecommerce.Payment.Infrastructure.Persistence;
using Ecommerce.Payment.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Payment.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPaymentInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddDbContext<PaymentDbContext>(opt => opt.UseInMemoryDatabase("ecommerce-payment"))
            .AddScoped<IPaymentRepository, PaymentRepository>()
            .AddScoped<IPaymentGateway, PaymentGateway>();

        return serviceCollection;
    }
}