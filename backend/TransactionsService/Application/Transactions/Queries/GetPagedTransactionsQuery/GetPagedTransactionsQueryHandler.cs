
using Application.Transactions.DTOs;
using Application.Transactions.Interfaces;
using Domain.Models;

namespace Application.Transactions.Queries.GetPagedTransactionsQuery;

public class GetPagedTransactionsQueryHandler : IRequestHandler<GetPagedTransactionsQuery, ErrorOr<PagedResponse<TransactionResponse>>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetPagedTransactionsQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
    }

    public async Task<ErrorOr<PagedResponse<TransactionResponse>>> Handle(GetPagedTransactionsQuery request, CancellationToken cancellationToken)
    {
        List<Transaction> items = await _transactionRepository.GetPagedFilteredAsync(
            request.ProductId,
            request.StartDate,
            request.EndDate,
            request.Type,
            request.Page,
            request.PageSize
        );

        int total = await _transactionRepository.CountFilteredAsync(
            request.ProductId,
            request.StartDate,
            request.EndDate,
            request.Type
        );

        return new PagedResponse<TransactionResponse>
        (
            items.Select(p => new TransactionResponse(
                p.Id.Value,
                p.Date,
                p.Type,
                p.ProductId,
                p.Quantity,
                p.UnitPrice,
                p.Detail
            )).ToList(),
            total
        );
    }
}
