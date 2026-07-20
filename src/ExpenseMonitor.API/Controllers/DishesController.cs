using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExpenseMonitor.Application.Dishes.Commands.CreateDishes;
using ExpenseMonitor.Application.Dishes.Commands.DeleteDishes;
using ExpenseMonitor.Application.Dishes.Dtos;
using ExpenseMonitor.Application.Dishes.Queries.GetByIdForRestaurant;
using ExpenseMonitor.Application.Dishes.Queries.GetDishesForRestaurant;
using ExpenseMonitor.Application.Restaurants.Dtos;
using ExpenseMonitor.Infrastructure.Authorization;

namespace ExpenseMonitor.API.Controllers
{
    [Route("api/restaurants/{restaurantId}/dishes")]
    [ApiController]
    [Authorize]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = PolicyName.AtLeast20)]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
        {
            var dishes =await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);
        }
        [HttpGet]
        [Route("{dishId}")]
        public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dishes = await mediator.Send(new GetDishesByIdRestaurantQuery(restaurantId, dishId));
            return Ok(dishes);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
            var dishId=await mediator.Send(command);
            return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId,  dishId }, null);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDishesForRestaurant([FromRoute] int restaurantId)
        {
            await mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));
            return NoContent();
        }
    }
}
