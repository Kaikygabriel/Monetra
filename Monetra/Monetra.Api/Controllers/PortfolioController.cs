using MediatR;
using Microsoft.AspNetCore.Mvc;
using Monetra.Application.UseCases.Portfolio.Commands.Request;
using Monetra.Application.UseCases.Portfolio.Query.Request;

namespace Monetra.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PortfolioController : ControllerBase
{
    private readonly IMediator _mediator;

    public PortfolioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> Get([FromQuery] GetPortfolioByCustumerIdRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    [HttpGet("ResultOfMonth")]
    public async Task<ActionResult> ResultOfMonth([FromQuery] GetValueResultOfMonthRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreatePortfolioRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Created() : BadRequest(result.Error);
    }
    [HttpPost("AddValue")]
    public async Task<ActionResult> AddValue([FromBody] AddedValuePortfolioRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }
    [HttpPost("RemoveValue")]
    public async Task<ActionResult> RemoveValue([FromBody] RemoveValuePortfolioRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? NoContent () : BadRequest(result.Error);
    }
}