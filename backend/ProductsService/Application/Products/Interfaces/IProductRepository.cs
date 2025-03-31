using Domain.Models;

namespace Application.Products.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(ProductId id);
    Task<bool> ExistAsync(ProductId id);
    Task<string> GetImageName(ProductId id);
    Task AddAsync(Product product);
    void Update(Product product);
    void Delete(Product product);
    Task<List<Product>> GetPagedAsync(int page, int pageSize, string? search);
    Task<int> CountAsync(string? search);
}
