using MediatR;
using ExpenseMonitor.Application.Categories.Dtos;

namespace ExpenseMonitor.Application.MonthlyBudgets.Queries.GetUnassignedCategoriesByUserId
{
    public class GetUnassignedCategoriesByUserIdQuery : IRequest<IEnumerable<CategoryDto>>
    {
    }
}