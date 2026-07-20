using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseMonitor.Application.User.Command;
using ExpenseMonitor.Application.Users.Command.AssignRole;
using ExpenseMonitor.Application.Users.Command.RemoveRole;
using ExpenseMonitor.Domain.Constants;

namespace ExpenseMonitor.API.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
        [HttpPost]
        [Route("userRole")]
        [Authorize(Roles = UserRoless.Admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
        [HttpDelete]
        [Route("userRole")]
        [Authorize(Roles = UserRoless.Admin)]

        public async Task<IActionResult> RemoveUserRole(RemoveUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}
