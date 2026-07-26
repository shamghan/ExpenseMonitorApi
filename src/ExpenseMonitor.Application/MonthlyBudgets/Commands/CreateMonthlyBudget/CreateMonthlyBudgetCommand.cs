using MediatR;
using ExpenseMonitor.Application.MonthlyBudgets.Dtos;

namespace ExpenseMonitor.Application.MonthlyBudgets.Commands.CreateMonthlyBudget
{
    public class CreateMonthlyBudgetCommand : IRequest<int>
    {
        public CreateMonthlyBudgetDto MonthlyBudgetDto { get; set; }
        public CreateMonthlyBudgetCommand()
        {
            MonthlyBudgetDto = new CreateMonthlyBudgetDto();
        }
    }
}