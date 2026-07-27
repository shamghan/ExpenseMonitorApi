using AutoMapper;
using ExpenseMonitor.Application.Categories.Commands.UpdateCategory;
using ExpenseMonitor.Domain.Entities;

namespace ExpenseMonitor.Application.Categories.Dtos
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<UpdateCategoryCommand, Category>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategorySummaryDto>();
        }
    }
}
