using Ecommerce.Inventory.Core.Commands;
using Ecommerce.Inventory.Shared.Events;
using Ecommerce.Shared.Abstractions.CQRS;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Inventory.Consumers;

public class ReserveStockConsumer : IConsumer<ReserveStock>
{
    private readonly IDispatcher _dispatcher;
    private readonly ILogger<ReserveStockConsumer> _logger;

    public ReserveStockConsumer(IDispatcher dispatcher, ILogger<ReserveStockConsumer> logger)
    {
        _dispatcher = dispatcher;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ReserveStock> context)
    {
        _logger.LogInformation($"ReserveStock '{context.Message.OrderId}'");
        
        await _dispatcher.SendAsync(new ReserveInventory(context.Message.OrderId, context.Message.Products));
    }
}