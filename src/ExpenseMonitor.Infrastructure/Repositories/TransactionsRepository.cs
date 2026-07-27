using Microsoft.EntityFrameworkCore;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Infrastructure.Persistence;

namespace ExpenseMonitor.Infrastructure.Repositories
{
    public class TransactionsRepository(RestaurantsDbContext dbContext) : ITransactionsRepository
    {
        public async Task<int> Create(Transaction entity)
        {
            await dbContext.Transactions.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(Transaction entity)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetByUserId(Guid userId)
        {
            var transactions = await dbContext.Transactions
                .Where(t => t.UserId == userId)
                .Include(t => t.Category)
                .ToListAsync();
            return transactions;
        }

        public async Task<Transaction?> GetById(int id, Guid userId)
        {
            var transaction = await dbContext.Transactions
                .Include(t => t.Category)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            return transaction;
        }

        public async Task SaveChanges()
            => await dbContext.SaveChangesAsync();
    }
}
