using AutoMapper;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Enums;

namespace ExpenseMonitor.Application.Transactions.Dtos
{
    public class TransactionsProfile : Profile
    {
        public TransactionsProfile()
        {
            CreateMap<CreateTransactionDto, Transaction>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => Enum.Parse<PaymentType>(src.PaymentType, true)));

            CreateMap<UpdateTransactionDto, Transaction>()
                .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => Enum.Parse<PaymentType>(src.PaymentType, true)));

            CreateMap<Transaction, TransactionDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId.ToString()))
                .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType.ToString()))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ForMember(dest => dest.CategoryIcon, opt => opt.MapFrom(src => src.Category != null ? src.Category.Icon : null))
                .ForMember(dest => dest.CategoryColorCode, opt => opt.MapFrom(src => src.Category != null ? src.Category.ColorCode : null));
        }
    }
}
