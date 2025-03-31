using Domain.Models;

namespace Application.Transactions.Queries.GetFilteredTransactionsQuery;

public record GetFilteredTransactionsQuery(
    Guid? ProductId,
    DateTime? StartDate,
    DateTime? EndDate,
    TransactionType? Type
) : IRequest<List<Transaction>>;
