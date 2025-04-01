using Domain.Models;

namespace Application.Transactions.Commands.Create;

public record CreateTransactionCommand(
    Guid ProductId,
    DateTime Date,
    TransactionType Type,
    int Quantity,
    decimal UnitPrice,
    string Detail) : IRequest<ErrorOr<Guid>>;
