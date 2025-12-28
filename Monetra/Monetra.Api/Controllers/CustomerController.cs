using MediatR;
using Microsoft.AspNetCore.Mvc;
using Monetra.Application.UseCases.Customer.Command.Request;
using Monetra.Application.UseCases.Custumer.Command.Request;

namespace Monetra.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] RegisterCostumerRequest  request)
    { 
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginCustomerRequest  request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}