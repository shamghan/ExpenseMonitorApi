using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Application.Categories.Dtos;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Exceptions;
using ExpenseMonitor.Domain.Repositories;

namespace ExpenseMonitor.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler(ILogger<GetCategoryByIdQueryHandler> logger,
        IMapper mapper, ICategoriesRepository categoriesRepository) : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting category by id {CategoryId}", request.Id);
            var category = await categoriesRepository.GetById(request.Id) ?? throw new NotFoundException(nameof(Category), request.Id.ToString());
            var categoryDto = mapper.Map<CategoryDto>(category);
            return categoryDto;
        }
    }
}
