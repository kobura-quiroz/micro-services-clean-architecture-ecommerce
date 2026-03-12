using Basket.Application.Commands;
using Basket.Application.DTOs;
using Basket.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IMediator _mediator;

    public BasketController(IMediator meditor)
    {
        _mediator = meditor;
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult<ShoppingCartDto>> GetBasket(string userName)
    {
        var query = new GetBasketByUserNameQuery(userName);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCartDto>> CreateOrUpdateBasket([FromBody] CreateShoppigCartCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{userName}")]
    public async Task<IActionResult> DeleteBasket(string userName)
    {
        var command = new DeleteBasketByUserNameCommand(userName);
        await _mediator.Send(command);
        return Ok();
    }
}
