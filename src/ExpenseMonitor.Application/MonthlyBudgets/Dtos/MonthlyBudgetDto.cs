namespace ExpenseMonitor.Application.MonthlyBudgets.Dtos
{
    public class MonthlyBudgetDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = default!;
        public int CategoryId { get; set; }
        public decimal MonthLimit { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryIcon { get; set; }
        public string? CategoryColorCode { get; set; }
    }
}