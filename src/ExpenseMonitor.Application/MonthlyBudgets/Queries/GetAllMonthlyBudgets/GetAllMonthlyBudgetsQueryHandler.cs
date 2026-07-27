using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Application.MonthlyBudgets.Dtos;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Application.User;

namespace ExpenseMonitor.Application.MonthlyBudgets.Queries.GetAllMonthlyBudgets
{
    public class GetAllMonthlyBudgetsQueryHandler(ILogger<GetAllMonthlyBudgetsQueryHandler> logger,
        IMapper mapper, IMonthlyBudgetsRepository monthlyBudgetsRepository, IUserContext userContext) : IRequestHandler<GetAllMonthlyBudgetsQuery, IEnumerable<MonthlyBudgetDto>>
    {
        public async Task<IEnumerable<MonthlyBudgetDto>> Handle(GetAllMonthlyBudgetsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all monthly budgets");
            var userIdString = userContext.GetCurrentUser()?.Id ?? throw new InvalidOperationException("User not authenticated");
            var userId = Guid.Parse(userIdString);
            var monthlyBudgets = await monthlyBudgetsRepository.GetByUserId(userId);
            var monthlyBudgetDtos = mapper.Map<IEnumerable<MonthlyBudgetDto>>(monthlyBudgets);
            return monthlyBudgetDtos;
        }
    }
}