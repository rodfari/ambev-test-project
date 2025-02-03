using FluentValidation;

namespace Application.Features.Sales.Commands.Delete;
public class DeleteSaleCommandValidator : AbstractValidator<DeleteSaleCommand>
{
    public DeleteSaleCommandValidator()
    {
        RuleFor(x => x.SaleId).NotEmpty();
    }
}
