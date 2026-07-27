using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Application.User;

namespace ExpenseMonitor.Application.MonthlyBudgets.Commands.CreateMonthlyBudget
{
    public class CreateMonthlyBudgetCommandHandler(ILogger<CreateMonthlyBudgetCommandHandler> logger,
        IMapper mapper, IMonthlyBudgetsRepository monthlyBudgetsRepository, IUserContext userContext) : IRequestHandler<CreateMonthlyBudgetCommand, int>
    {
        public async Task<int> Handle(CreateMonthlyBudgetCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new monthly budget {@MonthlyBudget}", request);
            var userIdString = userContext.GetCurrentUser()?.Id ?? throw new InvalidOperationException("User not authenticated");
            var userId = Guid.Parse(userIdString);
            var monthlyBudget = mapper.Map<MonthlyBudget>(request.MonthlyBudgetDto);
            monthlyBudget.UserId = userId;
            monthlyBudget.CreatedOn = DateTime.UtcNow;
            monthlyBudget.UpdatedOn = DateTime.UtcNow;
            var createdId = await monthlyBudgetsRepository.Create(monthlyBudget);
            return createdId;
        }
    }
}