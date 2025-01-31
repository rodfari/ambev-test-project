

using FluentValidation;

namespace Application.Features.Products.Command.Create;

public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        this.RuleFor(p => p.Title)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 50 characters.");
    }
}
