namespace ExpenseMonitor.Application.MonthlyBudgets.Dtos
{
    public class CreateMonthlyBudgetDto
    {
        public int CategoryId { get; set; }
        public decimal MonthLimit { get; set; }
    }
}