using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Application.Categories.Dtos;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Application.User;

namespace ExpenseMonitor.Application.MonthlyBudgets.Queries.GetUnassignedCategoriesByUserId
{
    public class GetUnassignedCategoriesByUserIdQueryHandler(ILogger<GetUnassignedCategoriesByUserIdQueryHandler> logger,
        IMapper mapper, IMonthlyBudgetsRepository monthlyBudgetsRepository, IUserContext userContext) : IRequestHandler<GetUnassignedCategoriesByUserIdQuery, IEnumerable<CategoryDto>>
    {
        public async Task<IEnumerable<CategoryDto>> Handle(GetUnassignedCategoriesByUserIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting unassigned categories for user");
            var userIdString = userContext.GetCurrentUser()?.Id ?? throw new InvalidOperationException("User not authenticated");
            var userId = Guid.Parse(userIdString);
            var categories = await monthlyBudgetsRepository.GetUnassignedCategoriesByUserId(userId);
            var categoryDtos = mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoryDtos;
        }
    }
}