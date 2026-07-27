using FluentValidation;
using ExpenseMonitor.Application.MonthlyBudgets.Dtos;

namespace ExpenseMonitor.Application.MonthlyBudgets.Validators
{
    public class CreateMonthlyBudgetDtoValidator : AbstractValidator<CreateMonthlyBudgetDto>
    {
        public CreateMonthlyBudgetDtoValidator()
        {
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Category ID must be greater than 0");

            RuleFor(x => x.MonthLimit)
                .GreaterThan(0).WithMessage("Month limit must be greater than 0");
        }
    }
}