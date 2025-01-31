using FluentValidation;

namespace Application.Features.Products.Command.Update;
public class UpdateProductCommandValidator: AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty().WithMessage("Id is required.");
    }
}