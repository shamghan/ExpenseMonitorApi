namespace ExpenseMonitor.Application.Transactions.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = default!;
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PaymentType { get; set; } = default!;
        public string? Notes { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryIcon { get; set; }
        public string? CategoryColorCode { get; set; }
    }
}
