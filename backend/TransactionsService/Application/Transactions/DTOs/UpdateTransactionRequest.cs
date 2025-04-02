
using Domain.Models;

namespace Application.Transactions.DTOs;

public record UpdateTransactionRequest(
    DateTime Date,
    TransactionType Type,
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    string Detail
);
