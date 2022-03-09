using Ecommerce.Inventory.Core.Commands;
using Ecommerce.Shared.Abstractions.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Inventory.Controllers;

[ApiController]
[Route($"api/inventories")]
internal class InventoryController : Controller
{
    private readonly IDispatcher _dispatcher;

    public InventoryController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateInventory request)
    {
        await _dispatcher.SendAsync(request);

        return Ok();
    }
}