using Application.Transactions.DTOs;

namespace Application.Transactions.Commands.Update;

public record UpdateTransactionCommand(
    Guid Id,
    UpdateTransactionRequest Data
) : IRequest<ErrorOr<Unit>>;
