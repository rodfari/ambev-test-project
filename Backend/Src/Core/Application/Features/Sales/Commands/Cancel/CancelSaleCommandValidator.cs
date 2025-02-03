using FluentValidation;

namespace Application.Features.Sales.Commands.Cancel;
public class CancelSaleCommandValidator : AbstractValidator<CancelSaleCommand>
{
    public CancelSaleCommandValidator()
    {
        RuleFor(x => x.SaleId).NotEmpty();
    }
}
