using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Application.Categories.Dtos;
using ExpenseMonitor.Domain.Repositories;

namespace ExpenseMonitor.Application.Categories.Queries.GetAllCategories
{
public class GetAllCategoriesQueryHandler(ILogger<GetAllCategoriesQueryHandler> logger,
    IMapper mapper, ICategoriesRepository categoriesRepository) : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategorySummaryDto>>
{
    public async Task<IEnumerable<CategorySummaryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all categories");
        var categories = await categoriesRepository.GetAllAsync();
        var categoryDtos = mapper.Map<IEnumerable<CategorySummaryDto>>(categories);
        return categoryDtos;
    }
}
}
