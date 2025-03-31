using Domain.Models;
using Application.Transactions.Interfaces;

namespace Application.Transactions.Queries.GetFilteredTransactionsQuery;

public class GetFilteredTransactionsQueryHandler : IRequestHandler<GetFilteredTransactionsQuery, List<Transaction>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetFilteredTransactionsQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
    }
    public async Task<List<Transaction>> Handle(GetFilteredTransactionsQuery request, CancellationToken cancellationToken)
    {
        return await _transactionRepository.GetFilteredAsync(
            request.ProductId,
            request.StartDate,
            request.EndDate,
            request.Type);
    }
}
