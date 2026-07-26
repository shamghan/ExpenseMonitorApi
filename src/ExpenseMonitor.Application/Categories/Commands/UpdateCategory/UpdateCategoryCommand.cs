using MediatR;

namespace ExpenseMonitor.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string ColorName { get; set; } = default!;
        public string ColorCode { get; set; } = default!;
        public string Icon { get; set; } = default!;
    }
}
