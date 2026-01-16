using Credo.Application.Queries.SubscriberQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Credo.Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class SubscriberController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubscriberController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("subscriber")]
    public async Task<IActionResult> GetSubscribers([FromQuery] int providerId, [FromQuery] string subscriberNumber)
    {
        var result = await _mediator.Send(new GetSubscriberQuery(providerId, subscriberNumber));
        
        return Ok(result);
    }
}