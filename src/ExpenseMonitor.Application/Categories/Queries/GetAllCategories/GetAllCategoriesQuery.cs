using MediatR;
using ExpenseMonitor.Application.Categories.Dtos;

namespace ExpenseMonitor.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategorySummaryDto>>
    {
    }
}
