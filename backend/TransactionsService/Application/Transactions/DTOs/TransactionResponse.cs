using Domain.Models;

namespace Application.Transactions.DTOs;
public record TransactionResponse(
    Guid Id,
    DateTime Date,
    TransactionType Type,
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    string Detail);
