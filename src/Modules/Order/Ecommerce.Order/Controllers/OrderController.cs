using Ecommerce.Order.Core.Commands;
using Ecommerce.Order.Core.Queries;
using Ecommerce.Shared.Abstractions.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Order.Controllers;

[ApiController]
[Route("api/orders")]
internal class OrderController : Controller
{
    private readonly IDispatcher _dispatcher;

    public OrderController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrder request)
    {
        await _dispatcher.SendAsync(request);

        return Ok();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _dispatcher.QueryAsync(new GetOrder(id));

        if (result is null)
            return NotFound();

        return Json(result);
    }
}

