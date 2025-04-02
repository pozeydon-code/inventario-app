using Application.Transactions.Commands.Create;
using Application.Transactions.Commands.Delete;
using Application.Transactions.Commands.Update;
using Application.Transactions.DTOs;
using Application.Transactions.Queries.GetFilteredTransactionsQuery;
using Application.Transactions.Queries.GetPagedTransactionsQuery;
using Application.Transactions.Queries.GetTransactionByIdQuery;
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

        return result.Match(
            transactionList => Ok(transactionList),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetTransactionByIdQuery(id));

        return result.Match(
            transaction => Ok(transaction),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTransactionRequest request)
    {
        var result = await _mediator.Send(new UpdateTransactionCommand(id, request));

        return result.Match(
            transaction => Ok(transaction),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteResult = await _mediator.Send(new DeleteTransactionCommand(id));

        return deleteResult.Match(
            result => Ok(StatusCodes.Status200OK),
            errors => Problem(errors)
        );
    }
}
