using FluentValidation;

namespace Application.Features.Products.Command.Delete
{
    public class DeleteProductCommandValidator: AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}