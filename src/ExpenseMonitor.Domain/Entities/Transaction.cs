using ExpenseMonitor.Domain.Enums;

namespace ExpenseMonitor.Domain.Entities;

public class Transaction
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int CategoryId { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime TransactionDate { get; set; }
    public PaymentType PaymentType { get; set; }
    public string? Notes { get; set; }

    public Category? Category { get; set; }
}
