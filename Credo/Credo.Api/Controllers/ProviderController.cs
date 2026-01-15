using Credo.Application.Queries.ProviderQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Credo.Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class ProviderController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProviderController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("provider")]
    public async Task<IActionResult> GetAsync([FromQuery] int id)
    {
        var result = await _mediator.Send(new GetProvidersQuery(id));

        return Ok(result);
    }
}