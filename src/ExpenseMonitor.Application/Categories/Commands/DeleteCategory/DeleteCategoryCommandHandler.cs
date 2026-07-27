using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Exceptions;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Application.User;

namespace ExpenseMonitor.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler(ILogger<DeleteCategoryCommandHandler> logger,
        ICategoriesRepository categoriesRepository, IUserContext userContext) : IRequestHandler<DeleteCategoryCommand>
    {
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting category with id: {CategoryId}", request.Id);
            var userIdString = userContext.GetCurrentUser()?.Id ?? throw new InvalidOperationException("User not authenticated");
            var userId = Guid.Parse(userIdString);
            var category = await categoriesRepository.GetById(request.Id);
            if (category == null)
                throw new NotFoundException(nameof(Category), request.Id.ToString());

            if (category.UserId != null && category.UserId != userId)
                throw new InvalidOperationException("You do not have permission to delete this category");

            await categoriesRepository.Delete(category);
        }
    }
}
