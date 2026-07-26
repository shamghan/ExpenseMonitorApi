using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Exceptions;
using ExpenseMonitor.Domain.Repositories;

namespace ExpenseMonitor.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler(ILogger<UpdateCategoryCommandHandler> logger,
        IMapper mapper, ICategoriesRepository categoriesRepository) : IRequestHandler<UpdateCategoryCommand>
    {
        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating category with id: {CategoryId} with {@UpdateCategory}", request.Id, request);
            var category = await categoriesRepository.GetById(request.Id);
            if (category == null)
                throw new NotFoundException(nameof(Category), request.Id.ToString());

            mapper.Map(request, category);
            await categoriesRepository.SaveChanges();
        }
    }
}
