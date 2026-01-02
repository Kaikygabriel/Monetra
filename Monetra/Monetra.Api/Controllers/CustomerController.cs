using MediatR;
using Microsoft.AspNetCore.Mvc;
using Monetra.Application.UseCases.Customer.Command.Request;
using Monetra.Application.UseCases.Customer.Command.Request.AlterPassword;
using Monetra.Application.UseCases.Customer.Query.Request;

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

    [HttpGet("Dashbord")]
    public async Task<ActionResult> DashBord([FromQuery] GetCustomerDashbordRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    [HttpPost("SendEmailForAlterPassword")]
    public async Task<ActionResult> AlterPassword([FromBody] SendEmailForAlterPasswordRequest  request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
    [HttpPost("AlterPasswordByToken")]
    public async Task<ActionResult> AlterPassword([FromBody] AlterPasswordCustomerRequest  request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}