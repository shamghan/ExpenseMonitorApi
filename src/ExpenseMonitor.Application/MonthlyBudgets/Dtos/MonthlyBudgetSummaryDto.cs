namespace ExpenseMonitor.Application.MonthlyBudgets.Dtos
{
    public class MonthlyBudgetSummaryDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;
        public decimal MonthLimit { get; set; }
    }
}