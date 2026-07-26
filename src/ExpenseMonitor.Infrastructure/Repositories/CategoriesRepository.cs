using Microsoft.EntityFrameworkCore;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Infrastructure.Persistence;

namespace ExpenseMonitor.Infrastructure.Repositories
{
    public class CategoriesRepository(RestaurantsDbContext dbContext) : ICategoriesRepository
    {
        public async Task<int> Create(Category entity)
        {
            await dbContext.Categories.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(Category entity)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categories = await dbContext.Categories
                .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Icon = c.Icon
                })
                .ToListAsync();
            return categories;
        }

        public async Task<Category?> GetById(int id)
        {
            var category = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return category;
        }

        public async Task SaveChanges()
            => await dbContext.SaveChangesAsync();
    }
}
