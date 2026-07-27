using ExpenseMonitor.Domain.Entities;

namespace ExpenseMonitor.Domain.Repositories
{
    public interface ITransactionsRepository
    {
        Task<int> Create(Transaction entity);
        Task Delete(Transaction entity);
        Task<IEnumerable<Transaction>> GetByUserId(Guid userId);
        Task<Transaction?> GetById(int id, Guid userId);
        Task SaveChanges();
    }
}
