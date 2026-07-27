using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ExpenseMonitor.Domain.Entities;

namespace ExpenseMonitor.Infrastructure.Persistence;



public class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options)
        : IdentityDbContext<User>(options)
{

public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<MonthlyBudget> MonthlyBudgets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>()
            .OwnsOne(r => r.Address);

        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Dishes)
            .WithOne()
            .HasForeignKey(d => d.RestaurantId);
       
        modelBuilder.Entity<User>()
            .HasMany(o => o.OwnedRestaurants)
            .WithOne(r => r.Owner)
            .HasForeignKey(r=>r.OwnerId);

        modelBuilder.Entity<MonthlyBudget>()
            .HasOne(mb => mb.Category)
            .WithMany()
            .HasForeignKey(mb => mb.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MonthlyBudget>()
            .HasIndex(mb => new { mb.UserId, mb.CategoryId })
            .IsUnique();

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Category)
            .WithMany()
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .Property(t => t.PaymentType)
            .HasConversion<string>();
    }
}


