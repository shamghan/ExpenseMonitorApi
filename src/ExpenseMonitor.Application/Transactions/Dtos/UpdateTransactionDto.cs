namespace ExpenseMonitor.Application.Transactions.Dtos
{
    public class UpdateTransactionDto
    {
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PaymentType { get; set; } = default!;
        public string? Notes { get; set; }
    }
}
