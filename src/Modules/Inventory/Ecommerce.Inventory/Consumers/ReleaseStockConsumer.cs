using Ecommerce.Inventory.Core.Commands;
using Ecommerce.Inventory.Shared.Events;
using Ecommerce.Shared.Abstractions.CQRS;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Inventory.Consumers;

public class ReleaseStockConsumer : IConsumer<ReleaseStock>
{
    private readonly IDispatcher _dispatcher;
    private readonly ILogger<ReleaseStockConsumer> _logger;

    public ReleaseStockConsumer(IDispatcher dispatcher, ILogger<ReleaseStockConsumer> logger)
    {
        _dispatcher = dispatcher;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ReleaseStock> context)
    {
        _logger.LogInformation($"ReleaseStock '{context.Message.OrderId}'");
        
        await _dispatcher.SendAsync(new ReleaseInventory(context.Message.Products));
    }
}