using MediatR;
using ExpenseMonitor.Application.MonthlyBudgets.Dtos;

namespace ExpenseMonitor.Application.MonthlyBudgets.Queries.GetAllMonthlyBudgets
{
    public class GetAllMonthlyBudgetsQuery : IRequest<IEnumerable<MonthlyBudgetDto>>
    {
    }
}