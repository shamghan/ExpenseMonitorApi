using ExpenseMonitor.Domain.Entities;

namespace ExpenseMonitor.Domain.Repositories
{
    public interface ICategoriesRepository
    {
        Task<int> Create(Category entity);
        Task Delete(Category entity);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetById(int id);
        Task SaveChanges();
    }
}
