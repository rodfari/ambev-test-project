using FluentValidation;

namespace Application.Features.SalesItems.Command;
public class CancelSaleItemCommandValidator : AbstractValidator<CancelSaleItemCommand>
{
    public CancelSaleItemCommandValidator()
    {
        RuleFor(x => x.SaleId).NotEmpty();
        RuleFor(x => x.ProductId).NotEmpty();
    }
}