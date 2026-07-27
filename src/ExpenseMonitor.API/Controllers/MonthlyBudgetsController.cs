using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseMonitor.Application.MonthlyBudgets.Dtos;
using ExpenseMonitor.Application.MonthlyBudgets.Commands.CreateMonthlyBudget;
using ExpenseMonitor.Application.MonthlyBudgets.Commands.UpdateMonthlyBudget;
using ExpenseMonitor.Application.MonthlyBudgets.Commands.DeleteMonthlyBudget;
using ExpenseMonitor.Application.MonthlyBudgets.Queries.GetAllMonthlyBudgets;
using ExpenseMonitor.Application.MonthlyBudgets.Queries.GetMonthlyBudgetById;
using ExpenseMonitor.Application.MonthlyBudgets.Queries.GetUnassignedCategoriesByUserId;
using ExpenseMonitor.Application.Categories.Dtos;

namespace ExpenseMonitor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MonthlyBudgetsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonthlyBudgetDto>>> GetAll()
        {
            var monthlyBudgets = await mediator.Send(new GetAllMonthlyBudgetsQuery());
            return Ok(monthlyBudgets);
        }

        [HttpGet("unassigned-categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetUnassignedCategories()
        {
            var categories = await mediator.Send(new GetUnassignedCategoriesByUserIdQuery());
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MonthlyBudgetDto>> GetById([FromRoute] int id)
        {
            var monthlyBudget = await mediator.Send(new GetMonthlyBudgetByIdQuery(id));
            return Ok(monthlyBudget);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateMonthlyBudgetDto createMonthlyBudgetDto)
        {
            var command = new CreateMonthlyBudgetCommand
            {
                MonthlyBudgetDto = createMonthlyBudgetDto
            };
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateMonthlyBudgetCommand command)
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
            await mediator.Send(new DeleteMonthlyBudgetCommand(id));
            return NoContent();
        }
    }
}