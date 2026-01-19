using Credo.Application.Commands.AutoPaymentAccountCommands;
using Credo.Application.Queries.AutoPaymentAccountsQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Credo.Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class AutoPaymentAccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AutoPaymentAccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("auto-payment-account")]
    public async Task<IActionResult> AddAsync([FromBody] AddAutoPaymentAccountModel model)
    {
        var result = await _mediator.Send(new AddAutoPaymentAccountCommand(
            model.CustomerSubscribtionId,
            model.AccountId,
            model.StartDate,
            model.EndDate,
            model.Amount,
            model.FrequencyInDays));

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _mediator.Send(new GetAutoPaymentAccounts());

        return Ok(result);
    }

    [HttpPut("update=auto-payment-account-amount")]
    public async Task<IActionResult> UpdateAsync([FromQuery] int id, [FromBody] decimal amount)
    {
        var result = await _mediator.Send(new UpdateAutoPaymentAccountAmounid, amount));

        return Ok(result);
    }

    [HttpDelete("delete-auto-payment-account")]
    public async Task<IActionResult> DeleteAsync([FromQuery] int id)
    {
        var result = await _mediator.Send(new DeleteAutPaymentAccountCommand(id));

        return Ok(result);
    }

    public record AddAutoPaymentAccountModel(
        int CustomerSubscribtionId,
        int AccountId,
        DateTime StartDate,
        DateTime EndDate,
        decimal Amount,
        int FrequencyInDays);
}