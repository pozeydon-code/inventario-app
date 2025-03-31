using System.Data.SqlTypes;
using Application.Data;
using Application.Products.Interfaces;
using Domain.Models;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IProductDbContext _productDbContext;

    public ProductRepository(IProductDbContext productDbContext)
    {
        _productDbContext = productDbContext ?? throw new ArgumentNullException(nameof(productDbContext));
    }

    public async Task AddAsync(Product product) => await _productDbContext.Products.AddAsync(product);

    public void Update(Product product) => _productDbContext.Products.Update(product);

    public void Delete(Product product) => _productDbContext.Products.Remove(product);

    public async Task<bool> ExistAsync(ProductId id) => await _productDbContext.Products.AnyAsync(product => product.Id == id);

    public async Task<List<Product>> GetAllAsync() => await _productDbContext.Products.ToListAsync();

    public async Task<Product?> GetByIdAsync(ProductId id) => await _productDbContext.Products.SingleOrDefaultAsync(product => product.Id == id);

    public async Task<List<Product>> GetPagedAsync(int page, int pageSize, string? search)
    {
        var query = _productDbContext.Products.AsQueryable();

        if (!string.IsNullOrEmpty(search))
            query = query.Where(p => ((string)p.Name).Contains(search));

        return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> CountAsync(string? search)
    {

        var query = _productDbContext.Products.AsQueryable();

        if (!string.IsNullOrEmpty(search))
            query = query.Where(p => ((string)p.Name).Contains(search));

        return await query.CountAsync();
    }

    public async Task<string> GetImageName(ProductId id)
    {
        return (await _productDbContext.Products.AsNoTracking().SingleOrDefaultAsync(product => product.Id == id))?.Image.Value!;
    }

}
