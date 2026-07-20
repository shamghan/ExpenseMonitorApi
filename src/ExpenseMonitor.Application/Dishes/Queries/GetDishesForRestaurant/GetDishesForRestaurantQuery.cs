using MediatR;
using ExpenseMonitor.Application.Dishes.Dtos;


namespace ExpenseMonitor.Application.Dishes.Queries.GetDishesForRestaurant;
public class GetDishesForRestaurantQuery(int restaurantId) : IRequest<IEnumerable<DishDto>>
{
    public int restaurantId { get; set; } = restaurantId;
}

