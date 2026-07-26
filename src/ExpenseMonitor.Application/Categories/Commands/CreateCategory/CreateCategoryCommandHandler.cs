using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Repositories;

namespace ExpenseMonitor.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler(ILogger<CreateCategoryCommandHandler> logger,
        IMapper mapper, ICategoriesRepository categoriesRepository) : IRequestHandler<CreateCategoryCommand, int>
    {
        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new category {@Category}", request);
            var category = mapper.Map<Category>(request.CategoryDto);
            var createdId = await categoriesRepository.Create(category);
            return createdId;
        }
    }
}
