using MediatR;

namespace ExpenseMonitor.Application.MonthlyBudgets.Commands.DeleteMonthlyBudget
{
    public class DeleteMonthlyBudgetCommand(int id) : IRequest
    {
        public int Id { get; set; } = id;
    }
}