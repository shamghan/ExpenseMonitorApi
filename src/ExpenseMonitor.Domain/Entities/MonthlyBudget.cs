namespace ExpenseMonitor.Domain.Entities;

public class MonthlyBudget
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int CategoryId { get; set; }
    public decimal MonthLimit { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    public Category? Category { get; set; }
}