using MediatR;
using ExpenseMonitor.Application.Categories.Dtos;

namespace ExpenseMonitor.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery(int id) : IRequest<CategoryDto>
    {
        public int Id { get; set; } = id;
    }
}
