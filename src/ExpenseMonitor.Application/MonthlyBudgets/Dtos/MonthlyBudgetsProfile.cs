using AutoMapper;
using ExpenseMonitor.Domain.Entities;

namespace ExpenseMonitor.Application.MonthlyBudgets.Dtos
{
    public class MonthlyBudgetsProfile : Profile
    {
        public MonthlyBudgetsProfile()
        {
            CreateMap<UpdateMonthlyBudgetDto, MonthlyBudget>();
            CreateMap<CreateMonthlyBudgetDto, MonthlyBudget>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedOn, opt => opt.MapFrom(_ => DateTime.UtcNow));
            CreateMap<MonthlyBudget, MonthlyBudgetDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ForMember(dest => dest.CategoryIcon, opt => opt.MapFrom(src => src.Category != null ? src.Category.Icon : null))
                .ForMember(dest => dest.CategoryColorCode, opt => opt.MapFrom(src => src.Category != null ? src.Category.ColorCode : null));
        }
    }
}