using FluentValidation;
using ExpenseMonitor.Application.Transactions.Dtos;
using ExpenseMonitor.Domain.Enums;

namespace ExpenseMonitor.Application.Transactions.Validators
{
    public class CreateTransactionDtoValidator : AbstractValidator<CreateTransactionDto>
    {
        public CreateTransactionDtoValidator()
        {
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Category ID must be greater than 0");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0");

            RuleFor(x => x.PaymentType)
                .NotEmpty().WithMessage("Payment type is required")
                .Must(BeValidPaymentType).WithMessage("Invalid payment type");
        }

        private bool BeValidPaymentType(string paymentType)
        {
            return Enum.TryParse<PaymentType>(paymentType, ignoreCase: true, out _);
        }
    }
}
