using System.Net.Http.Json;
using TransactionsService.Application.ProductsGateway;

namespace TransactionsService.Infrastructure.Services;

public class ProductsApiClient : IProductsApiClient
{
    private readonly HttpClient _httpClient;

    public ProductsApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int?> GetStockAsync(Guid productId)
    {
        var response = await _httpClient.GetAsync($"/api/products/{productId}");
        if (!response.IsSuccessStatusCode)
            return null;

        var product = await response.Content.ReadFromJsonAsync<ProductDto>();
        return product?.Stock;
    }

    public async Task<bool> UpdateStockAsync(Guid productId, int newStock)
    {
        var payload = new { stock = newStock };
        var response = await _httpClient.PatchAsync($"/api/products/{productId}/stock", JsonContent.Create(payload));
        return response.IsSuccessStatusCode;
    }

    private class ProductDto
    {
        public int Stock { get; set; }
    }
}
