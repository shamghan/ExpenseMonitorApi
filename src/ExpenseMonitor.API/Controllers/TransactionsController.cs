using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseMonitor.Application.Transactions.Dtos;
using ExpenseMonitor.Application.Transactions.Commands.CreateTransaction;
using ExpenseMonitor.Application.Transactions.Commands.UpdateTransaction;
using ExpenseMonitor.Application.Transactions.Commands.DeleteTransaction;
using ExpenseMonitor.Application.Transactions.Queries.GetAllTransactions;
using ExpenseMonitor.Application.Transactions.Queries.GetTransactionById;

namespace ExpenseMonitor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAll()
        {
            var transactions = await mediator.Send(new GetAllTransactionsQuery());
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDto>> GetById([FromRoute] int id)
        {
            var transaction = await mediator.Send(new GetTransactionByIdQuery(id));
            return Ok(transaction);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateTransactionDto createTransactionDto)
        {
            var command = new CreateTransactionCommand
            {
                TransactionDto = createTransactionDto
            };
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateTransactionCommand command)
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
            await mediator.Send(new DeleteTransactionCommand(id));
            return NoContent();
        }
    }
}
