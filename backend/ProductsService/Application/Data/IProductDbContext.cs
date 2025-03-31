using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IProductDbContext
{
    DbSet<Product> Products { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
