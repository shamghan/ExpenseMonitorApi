using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExpenseMonitor.Application.Restaurants;
using ExpenseMonitor.Application.Restaurants.Command.CreateRestaurant;
using ExpenseMonitor.Application.Restaurants.Commands.DeleteRestaurant;
using ExpenseMonitor.Application.Restaurants.Commands.UpdateRestuarant;
using ExpenseMonitor.Application.Restaurants.Commands.UploadRestaurantLogo;
using ExpenseMonitor.Application.Restaurants.Dtos;
using ExpenseMonitor.Application.Restaurants.Queries.GetAllRestaurants;
using ExpenseMonitor.Application.Restaurants.Queries.GetRestaurantById;
using ExpenseMonitor.Domain.Constants;
using ExpenseMonitor.Infrastructure.Authorization;
using System.Reflection;

namespace ExpenseMonitor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        //[Authorize(Policy = PolicyName.CreatedAtLeast2Restaurants)]
        public async Task<ActionResult<IEnumerable<RestaurantsDto>>> GetAll([FromQuery] GetAllRestaurantQuery query)
        {
            var restaurant = await mediator.Send(query);
            return Ok(restaurant);
        }
        [HttpGet]
        [Route("{id}")]
        [Authorize(Policy = PolicyName.HasNationality)]
        public async Task<ActionResult<RestaurantsDto>> GetById([FromRoute] int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
            return Ok(restaurant);
        }
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));
            return NoContent();
        }

        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateById([FromRoute] int id, UpdateRestuarantCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = UserRoless.Owner)]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
        {
            var command = new CreateRestaurantCommand
            {
                RestaurantDto = createRestaurantDto
            };
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);

        }
        [HttpPost("{id}/logo")]
        [Authorize(Roles = UserRoless.Owner)]
        public async Task<IActionResult> UploadLogo([FromRoute] int id, IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var command = new UploadRestaurantLogoCommand
            {
                RestaurantId = id,
                FileName = $"{id}-{file.FileName}",
                File = stream
            };
            await mediator.Send(command);
            return NoContent();

        }
    }
}
