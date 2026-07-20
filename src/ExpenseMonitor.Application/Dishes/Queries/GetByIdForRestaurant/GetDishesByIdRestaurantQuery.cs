using MediatR;
using ExpenseMonitor.Application.Dishes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseMonitor.Application.Dishes.Queries.GetByIdForRestaurant
{
    public class GetDishesByIdRestaurantQuery(int restaurantId, int dishId) :IRequest<DishDto>
    {
        public int RestaurantId { get; set; } = restaurantId;
        public int DishId { get; set; } = dishId;
    }
}
