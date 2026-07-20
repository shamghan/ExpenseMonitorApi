using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Seeders
{
    public class RestaurantsSeeder(RestaurantsDbContext dbContext, UserManager<User> userManager) : IRestaurantsSeeder
    {
        public async Task Seed()
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                await dbContext.Database.MigrateAsync();
            }

            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Restaurants.Any())
                {
                    var owner = await userManager.FindByEmailAsync("owner@gmail.com");
                    if (owner == null)
                    {
                        owner = new User
                        {
                            Email = "owner@gmail.com",
                            UserName = "owner@gmail.com"
                        };
                        await userManager.CreateAsync(owner, "Owner123!");
                    }

                    var restaurants = GetRestaurants(owner.Id);
                    dbContext.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roless =
                [
                new  (UserRoless.User)
                {
                    NormalizedName= UserRoless.User.ToUpper()
                },
                new  (UserRoless.Admin)
                {
                    NormalizedName= UserRoless.Admin.ToUpper()
                },
                new  (UserRoless.Owner)
                {
                    NormalizedName= UserRoless.Owner.ToUpper()
                }
                ];
            return roless;
        }
        private IEnumerable<Restaurant> GetRestaurants(string ownerId)
        {
            List<Restaurant> restaurants = [
                 new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                OwnerId = ownerId,
                Dishes =
                [
                    new ()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M,
                    },

                    new ()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M,
                    },
                ],
                Address = new ()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                }
            },
            new ()
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                OwnerId = ownerId,
                Address = new Address()
                {
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F 8SR"
                }
            }
                ];
            return restaurants;
        }
    }
}
