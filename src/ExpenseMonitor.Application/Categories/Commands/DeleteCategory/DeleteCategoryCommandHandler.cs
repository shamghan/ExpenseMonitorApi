using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Exceptions;
using ExpenseMonitor.Domain.Repositories;

namespace ExpenseMonitor.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler(ILogger<DeleteCategoryCommandHandler> logger,
        ICategoriesRepository categoriesRepository) : IRequestHandler<DeleteCategoryCommand>
    {
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting category with id: {CategoryId}", request.Id);
            var category = await categoriesRepository.GetById(request.Id);
            if (category == null)
                throw new NotFoundException(nameof(Category), request.Id.ToString());

            await categoriesRepository.Delete(category);
        }
    }
}
