using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Application.Categories.Dtos;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Application.User;

namespace ExpenseMonitor.Application.Categories.Queries.GetAllCategories
{
public class GetAllCategoriesQueryHandler(ILogger<GetAllCategoriesQueryHandler> logger,
    IMapper mapper, ICategoriesRepository categoriesRepository, IUserContext userContext) : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategorySummaryDto>>
{
    public async Task<IEnumerable<CategorySummaryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all categories");
        var userIdString = userContext.GetCurrentUser()?.Id ?? throw new InvalidOperationException("User not authenticated");
        var userId = Guid.Parse(userIdString);
        var categories = await categoriesRepository.GetAllAsync(userId);
        var categoryDtos = mapper.Map<IEnumerable<CategorySummaryDto>>(categories);
        return categoryDtos;
    }
}
}
