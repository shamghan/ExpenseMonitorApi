using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseMonitor.Application.Categories.Dtos;
using ExpenseMonitor.Application.Categories.Commands.CreateCategory;
using ExpenseMonitor.Application.Categories.Commands.UpdateCategory;
using ExpenseMonitor.Application.Categories.Commands.DeleteCategory;
using ExpenseMonitor.Application.Categories.Queries.GetAllCategories;
using ExpenseMonitor.Application.Categories.Queries.GetCategoryById;

namespace ExpenseMonitor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CategorySummaryDto>>> GetAll()
        {
            var categories = await mediator.Send(new GetAllCategoriesQuery());
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById([FromRoute] int id)
        {
            var category = await mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(category);
        }

        [HttpPost]

        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto createCategoryDto)
        {
            var command = new CreateCategoryCommand
            {
                CategoryDto = createCategoryDto
            };
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateCategoryCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await mediator.Send(new DeleteCategoryCommand(id));
            return NoContent();
        }
    }
}
