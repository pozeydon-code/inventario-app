namespace TransactionsService.Application.ProductsGateway;

public interface IProductsApiClient
{
    Task<int?> GetStockAsync(Guid productId);
    Task<bool> UpdateStockAsync(Guid productId, int newStock);
}
