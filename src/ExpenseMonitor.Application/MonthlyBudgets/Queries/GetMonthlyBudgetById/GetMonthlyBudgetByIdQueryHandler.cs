using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Application.MonthlyBudgets.Dtos;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Exceptions;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Application.User;

namespace ExpenseMonitor.Application.MonthlyBudgets.Queries.GetMonthlyBudgetById
{
    public class GetMonthlyBudgetByIdQueryHandler(ILogger<GetMonthlyBudgetByIdQueryHandler> logger,
        IMapper mapper, IMonthlyBudgetsRepository monthlyBudgetsRepository, IUserContext userContext) : IRequestHandler<GetMonthlyBudgetByIdQuery, MonthlyBudgetDto>
    {
        public async Task<MonthlyBudgetDto> Handle(GetMonthlyBudgetByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting monthly budget by id {MonthlyBudgetId}", request.Id);
            var userIdString = userContext.GetCurrentUser()?.Id ?? throw new InvalidOperationException("User not authenticated");
            var userId = Guid.Parse(userIdString);
            var monthlyBudget = await monthlyBudgetsRepository.GetById(request.Id, userId) ?? throw new NotFoundException(nameof(MonthlyBudget), request.Id.ToString());
            var monthlyBudgetDto = mapper.Map<MonthlyBudgetDto>(monthlyBudget);
            return monthlyBudgetDto;
        }
    }
}