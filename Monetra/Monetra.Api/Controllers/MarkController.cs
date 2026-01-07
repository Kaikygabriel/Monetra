using MediatR;
using Microsoft.AspNetCore.Mvc;
using Monetra.Application.UseCases.Mark.Command.Request;

namespace Monetra.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MarkController : ControllerBase
{
    private readonly IMediator _mediator;

    public MarkController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody]CreateMarkRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Created() : BadRequest(result.Error);
    }
    [HttpDelete]
    public async Task<ActionResult> Delete([FromQuery]DeleteMarkRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Created() : BadRequest(result.Error);
    }
}