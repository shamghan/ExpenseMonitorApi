using Microsoft.EntityFrameworkCore;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Infrastructure.Persistence;

namespace ExpenseMonitor.Infrastructure.Repositories
{
    public class MonthlyBudgetsRepository(RestaurantsDbContext dbContext) : IMonthlyBudgetsRepository
    {
        public async Task<int> Create(MonthlyBudget entity)
        {
            await dbContext.MonthlyBudgets.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(MonthlyBudget entity)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MonthlyBudget>> GetByUserId(Guid userId)
        {
            var budgets = await dbContext.MonthlyBudgets
                .Where(mb => mb.UserId == userId)
                .Include(mb => mb.Category)
                .Select(mb => new MonthlyBudget
                {
                    Id = mb.Id,
                    UserId = mb.UserId,
                    CategoryId = mb.CategoryId,
                    MonthLimit = mb.MonthLimit,
                    CreatedOn = mb.CreatedOn,
                    UpdatedOn = mb.UpdatedOn,
                    Category = mb.Category
                })
                .ToListAsync();
            return budgets;
        }

        public async Task<IEnumerable<Category>> GetUnassignedCategoriesByUserId(Guid userId)
        {
            var categories = await dbContext.Categories
                .Where(c => (c.UserId == null || c.UserId == userId) && !dbContext.MonthlyBudgets
                    .Any(mb => mb.CategoryId == c.Id && mb.UserId == userId))
                .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Icon = c.Icon,
                    ColorCode= c.ColorCode,
                    ColorName = c.ColorName,
                    UserId = c.UserId
                })
                .ToListAsync();
            return categories;
        }

        public async Task<MonthlyBudget?> GetById(int id, Guid userId)
        {
            var budget = await dbContext.MonthlyBudgets
                .Include(mb => mb.Category)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            return budget;
        }

        public async Task<MonthlyBudget?> GetByCategoryId(Guid userId, int categoryId)
        {
            var budget = await dbContext.MonthlyBudgets
                .Include(mb => mb.Category)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.CategoryId == categoryId);
            return budget;
        }

        public async Task SaveChanges()
            => await dbContext.SaveChangesAsync();
    }
}