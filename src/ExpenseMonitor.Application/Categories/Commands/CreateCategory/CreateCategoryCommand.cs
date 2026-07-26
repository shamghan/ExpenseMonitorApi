using MediatR;
using ExpenseMonitor.Application.Categories.Dtos;

namespace ExpenseMonitor.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public CreateCategoryDto CategoryDto { get; set; }
        public CreateCategoryCommand()
        {
            CategoryDto = new CreateCategoryDto();
        }
    }
}
