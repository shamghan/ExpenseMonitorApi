namespace ExpenseMonitor.Application.Transactions.Dtos
{
    public class CreateTransactionDto
    {
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PaymentType { get; set; } = default!;
        public string? Notes { get; set; }
    }
}
