using ExpenseMonitor.Domain.Entities;

namespace ExpenseMonitor.Domain.Repositories
{
    public interface IMonthlyBudgetsRepository
    {
        Task<int> Create(MonthlyBudget entity);
        Task Delete(MonthlyBudget entity);
        Task<IEnumerable<MonthlyBudget>> GetByUserId(Guid userId);
        Task<IEnumerable<Category>> GetUnassignedCategoriesByUserId(Guid userId);
        Task<MonthlyBudget?> GetById(int id, Guid userId);
        Task<MonthlyBudget?> GetByCategoryId(Guid userId, int categoryId);
        Task SaveChanges();
    }
}