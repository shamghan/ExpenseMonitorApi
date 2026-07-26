namespace ExpenseMonitor.Application.Categories.Dtos
{
    public class CategorySummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? Icon { get; set; }
    }
}
