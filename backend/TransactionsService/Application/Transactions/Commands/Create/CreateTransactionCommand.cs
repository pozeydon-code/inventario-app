using Microsoft.AspNetCore.Http;
using Domain.Models;

namespace Application.Transactions.Commands.Create;

public record CreateTransactionCommand(
    string ProductId,
    DateTime Date,
    TransactionType Type,
    int Quantity,
    decimal UnitPrice,
    string Detail) : IRequest<ErrorOr<Guid>>;
