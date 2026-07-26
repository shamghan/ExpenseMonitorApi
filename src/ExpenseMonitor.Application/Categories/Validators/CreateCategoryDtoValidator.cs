using FluentValidation;
using ExpenseMonitor.Application.Categories.Dtos;

namespace ExpenseMonitor.Application.Categories.Validators;

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .Length(2, 100);

        RuleFor(dto => dto.ColorName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(dto => dto.ColorCode)
            .NotEmpty()
            .Matches(@"^#[0-9A-Fa-f]{6}$")
            .WithMessage("ColorCode must be a valid hex color (e.g., #FF9800).");

        RuleFor(dto => dto.Icon)
            .NotEmpty()
            .MaximumLength(50);
    }
}
