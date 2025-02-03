
using Application.Dtos;
using FluentValidation;

namespace Application.Features.Sales.Commands.Create;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.BranchId).NotEmpty();
        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("At least one item is required.");

        RuleForEach(x => x.Items).SetValidator(new CreateSaleItemValidator());
    }
}

public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemDto>
{
    public CreateSaleItemValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be at least 1.")
            .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 of the same item.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
    }
}
