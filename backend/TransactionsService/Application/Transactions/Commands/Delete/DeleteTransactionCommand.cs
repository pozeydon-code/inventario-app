namespace Application.Transactions.Commands.Delete;

public record DeleteTransactionCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
