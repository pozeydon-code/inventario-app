namespace Application.Products.Commands.Update.UpdateStock;
public record UpdateStockCommand(Guid Id, int Stock) : IRequest<ErrorOr<Unit>>;