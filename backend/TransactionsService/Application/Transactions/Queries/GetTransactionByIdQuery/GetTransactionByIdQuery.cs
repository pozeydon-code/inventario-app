using Application.Transactions.DTOs;

namespace Application.Transactions.Queries.GetTransactionByIdQuery;

public record GetTransactionByIdQuery(Guid Id) : IRequest<ErrorOr<TransactionResponse>>;
