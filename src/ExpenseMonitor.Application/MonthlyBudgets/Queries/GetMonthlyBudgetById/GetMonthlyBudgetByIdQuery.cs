using MediatR;
using ExpenseMonitor.Application.MonthlyBudgets.Dtos;

namespace ExpenseMonitor.Application.MonthlyBudgets.Queries.GetMonthlyBudgetById
{
    public class GetMonthlyBudgetByIdQuery(int id) : IRequest<MonthlyBudgetDto>
    {
        public int Id { get; set; } = id;
    }
}