using Application.Transactions.DTOs;
using Domain.Models;

namespace Application.Transactions.Queries.GetPagedTransactionsQuery;

public record GetPagedTransactionsQuery(
    Guid? ProductId,
    DateTime? StartDate,
    DateTime? EndDate,
    TransactionType? Type,
    int Page,
    int PageSize
) : IRequest<ErrorOr<PagedResponse<TransactionResponse>>>;
