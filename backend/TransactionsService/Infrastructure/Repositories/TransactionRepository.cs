using System.Data.SqlTypes;
using Application.Data;
using Application.Transactions.Interfaces;
using Domain.Models;

namespace Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly ITransactionDbContext _transactionDbContext;

    public TransactionRepository(ITransactionDbContext transactionDbContext)
    {
        _transactionDbContext = transactionDbContext ?? throw new ArgumentNullException(nameof(TransactionDbContext));
    }

    public async Task AddAsync(Transaction Transaction) => await _transactionDbContext.Transactions.AddAsync(Transaction);

    public void Update(Transaction Transaction) => _transactionDbContext.Transactions.Update(Transaction);

    public void Delete(Transaction Transaction) => _transactionDbContext.Transactions.Remove(Transaction);

    public async Task<bool> ExistAsync(TransactionId id) => await _transactionDbContext.Transactions.AnyAsync(transaction => transaction.Id == id);
    public async Task<bool> ExistByProductIdAsync(Guid productId) => await _transactionDbContext.Transactions.AnyAsync(transaction => transaction.ProductId == productId);

    public async Task<List<Transaction>> GetAllAsync() => await _transactionDbContext.Transactions.ToListAsync();

    public async Task<Transaction?> GetByIdAsync(TransactionId id) => await _transactionDbContext.Transactions.SingleOrDefaultAsync(transaction => transaction.Id == id);

    public async Task<List<Transaction>> GetByProductIdAsync(Guid productId) => await _transactionDbContext.Transactions
                                                                                            .Where(t => t.ProductId == productId)
                                                                                            .ToListAsync();
    
    public async Task<List<Transaction>> GetFilteredAsync(
        Guid? productId,
        DateTime? startDate,
        DateTime? endDate,
        TransactionType? type)
    {
        var query = _transactionDbContext.Transactions.AsQueryable();

        if (productId.HasValue)
            query = query.Where(t => t.ProductId == productId.Value);

        if (startDate.HasValue)
            query = query.Where(t => t.Date >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(t => t.Date <= endDate.Value);

        if (type.HasValue)
            query = query.Where(t => t.Type == type.Value);

        return await query
            .OrderByDescending(t => t.Date)
            .ToListAsync();
    }
    
}
