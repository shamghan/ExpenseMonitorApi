using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Domain.Exceptions;
using ExpenseMonitor.Application.User;

namespace ExpenseMonitor.Application.MonthlyBudgets.Commands.UpdateMonthlyBudget
{
    public class UpdateMonthlyBudgetCommandHandler(ILogger<UpdateMonthlyBudgetCommandHandler> logger,
        IMapper mapper, IMonthlyBudgetsRepository monthlyBudgetsRepository, IUserContext userContext) : IRequestHandler<UpdateMonthlyBudgetCommand>
    {
        public async Task Handle(UpdateMonthlyBudgetCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating monthly budget with id {MonthlyBudgetId}", request.Id);
            var userIdString = userContext.GetCurrentUser()?.Id ?? throw new InvalidOperationException("User not authenticated");
            var userId = Guid.Parse(userIdString);
            var monthlyBudget = await monthlyBudgetsRepository.GetById(request.Id, userId) ?? throw new NotFoundException(nameof(MonthlyBudget), request.Id.ToString());
            monthlyBudget.MonthLimit = request.MonthlyBudgetDto.MonthLimit;

            monthlyBudget.UpdatedOn = DateTime.UtcNow;
            await monthlyBudgetsRepository.SaveChanges();
        }
    }
}