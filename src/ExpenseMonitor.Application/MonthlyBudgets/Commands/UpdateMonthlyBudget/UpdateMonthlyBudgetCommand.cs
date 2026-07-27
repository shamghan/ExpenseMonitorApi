using MediatR;
using ExpenseMonitor.Application.MonthlyBudgets.Dtos;

namespace ExpenseMonitor.Application.MonthlyBudgets.Commands.UpdateMonthlyBudget
{
    public class UpdateMonthlyBudgetCommand : IRequest
    {
        public int Id { get; set; }
        public UpdateMonthlyBudgetDto MonthlyBudgetDto { get; set; }
        public UpdateMonthlyBudgetCommand()
        {
            MonthlyBudgetDto = new UpdateMonthlyBudgetDto();
        }
    }
}