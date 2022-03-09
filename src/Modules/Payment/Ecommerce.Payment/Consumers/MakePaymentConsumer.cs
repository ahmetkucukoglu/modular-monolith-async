using Ecommerce.Payment.Core.Commands;
using Ecommerce.Payment.Shared.Events;
using Ecommerce.Shared.Abstractions.CQRS;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Payment.Consumers;

public class MakePaymentConsumer : IConsumer<MakePayment>
{
    private readonly IDispatcher _dispatcher;
    private readonly ILogger<MakePayment> _logger;

    public MakePaymentConsumer(IDispatcher dispatcher, ILogger<MakePayment> logger)
    {
        _dispatcher = dispatcher;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<MakePayment> context)
    {
        _logger.LogInformation($"MakePayment '{context.Message.OrderId}'");

        await _dispatcher.SendAsync(new CreatePayment(context.Message.OrderId, context.Message.TotalPrice,
            context.Message.Currency));
    }
}