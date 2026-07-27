using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Application.User;

namespace ExpenseMonitor.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler(ILogger<CreateCategoryCommandHandler> logger,
        IMapper mapper, ICategoriesRepository categoriesRepository, IUserContext userContext) : IRequestHandler<CreateCategoryCommand, int>
    {
        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new category {@Category}", request);
            var userIdString = userContext.GetCurrentUser()?.Id ?? throw new InvalidOperationException("User not authenticated");
            var userId = Guid.Parse(userIdString);
            var category = mapper.Map<Category>(request.CategoryDto);
            category.UserId = userId;
            var createdId = await categoriesRepository.Create(category);
            return createdId;
        }
    }
}
