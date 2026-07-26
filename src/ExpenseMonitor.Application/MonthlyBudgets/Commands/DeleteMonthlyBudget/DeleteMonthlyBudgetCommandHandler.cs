using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Domain.Exceptions;
using ExpenseMonitor.Application.User;

namespace ExpenseMonitor.Application.MonthlyBudgets.Commands.DeleteMonthlyBudget
{
    public class DeleteMonthlyBudgetCommandHandler(ILogger<DeleteMonthlyBudgetCommandHandler> logger,
        IMonthlyBudgetsRepository monthlyBudgetsRepository, IUserContext userContext) : IRequestHandler<DeleteMonthlyBudgetCommand>
    {
        public async Task Handle(DeleteMonthlyBudgetCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting monthly budget with id {MonthlyBudgetId}", request.Id);
            var userIdString = userContext.GetCurrentUser()?.Id ?? throw new InvalidOperationException("User not authenticated");
            var userId = Guid.Parse(userIdString);
            var monthlyBudget = await monthlyBudgetsRepository.GetById(request.Id, userId) ?? throw new NotFoundException(nameof(MonthlyBudget), request.Id.ToString());
            await monthlyBudgetsRepository.Delete(monthlyBudget);
        }
    }
}