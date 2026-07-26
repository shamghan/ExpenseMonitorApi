namespace ExpenseMonitor.Domain.Entities;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string ColorName { get; set; } = default!;
    public string ColorCode { get; set; } = default!;
    public string Icon { get; set; } = default!;
}
