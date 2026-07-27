using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Exceptions;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Application.User;

namespace ExpenseMonitor.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler(ILogger<UpdateCategoryCommandHandler> logger,
        IMapper mapper, ICategoriesRepository categoriesRepository, IUserContext userContext) : IRequestHandler<UpdateCategoryCommand>
    {
        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating category with id: {CategoryId} with {@UpdateCategory}", request.Id, request);
            var userIdString = userContext.GetCurrentUser()?.Id ?? throw new InvalidOperationException("User not authenticated");
            var userId = Guid.Parse(userIdString);
            var category = await categoriesRepository.GetById(request.Id);
            if (category == null)
                throw new NotFoundException(nameof(Category), request.Id.ToString());

            if (category.UserId != null && category.UserId != userId)
                throw new InvalidOperationException("You do not have permission to update this category");

            mapper.Map(request, category);
            await categoriesRepository.SaveChanges();
        }
    }
}
