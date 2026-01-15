using Credo.Application.Queries.ProviderCategoryQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Credo.Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class ProviderCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProviderCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("provider-categories")]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _mediator.Send(new GetProviderCategoriesQuery());
        
        return Ok(result);
    }
}