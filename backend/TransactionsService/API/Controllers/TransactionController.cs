using Application.Transactions.Commands.Create;
using Application.Transactions.Queries.GetFilteredTransactionsQuery;
using Application.Transactions.Queries.GetPagedTransactionsQuery;
using Domain.Models;

namespace API.Controllers;

[Route("api/transactions")]
public class Transactions : ApiController
{
    private readonly ISender _mediator;

    public Transactions(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionCommand commandRequest)
    {
        var createResult = await _mediator.Send(commandRequest);

        return createResult.Match(
            productId => Ok(productId),
            errors => Problem(errors)
            );
    }

    [HttpGet]
    public async Task<IActionResult> GetFiltered(
        [FromQuery] Guid? productId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] TransactionType? type)
    {
        var result = await _mediator.Send(new GetFilteredTransactionsQuery
        (
            productId,
            startDate,
            endDate,
            type
        ));

        return Ok(result);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged([FromQuery] Guid? productId,
                                              [FromQuery] DateTime? startDate,
                                              [FromQuery] DateTime? endDate,
                                              [FromQuery] TransactionType? type,
                                              [FromQuery] int page = 1,
                                              [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetPagedTransactionsQuery(
            productId,
            startDate,
            endDate,
            type,
            page,
            pageSize
        ));

        return Ok(result);
    }
}
