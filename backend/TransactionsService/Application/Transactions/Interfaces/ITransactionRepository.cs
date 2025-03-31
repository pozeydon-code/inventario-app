using Domain.Models;

namespace Application.Transactions.Interfaces;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetAllAsync();
    Task<Transaction?> GetByIdAsync(TransactionId id);
    Task<List<Transaction>> GetByProductIdAsync(Guid productId);
    Task<bool> ExistByProductIdAsync(Guid productId);
    Task<bool> ExistAsync(TransactionId id);
    Task AddAsync(Transaction Transaction);
    void Update(Transaction Transaction);
    void Delete(Transaction Transaction);
    Task<List<Transaction>> GetFilteredAsync(
        Guid? productId,
        DateTime? startDate,
        DateTime? endDate,
        TransactionType? type);
}
