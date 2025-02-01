using Domain.Contracts;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Sales.Commands.Cancel;
public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, Unit>
{
    private readonly ISaleRepository _saleRepository;

    public CancelSaleCommandHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<Unit> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.SaleId);
        if (sale == null) throw new NotFoundException("Sale not found");

        sale.Cancel();
        await _saleRepository.UpdateAsync(sale);
        return Unit.Value;
    }
}
