using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface ITransactionDbContext
{
    DbSet<Transaction> Transactions { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
