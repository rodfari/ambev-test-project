using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.SalesItems.Command;

public class CancelSaleItemCommandHandler : IRequestHandler<CancelSaleItemCommand, Unit>
{
    private readonly ISaleRepository _saleRepository;

    public CancelSaleItemCommandHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<Unit> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.SaleId);
        if (sale == null) throw new NotFoundException("Sale not found");

        sale.CancelItem(request.ProductId);
        await _saleRepository.UpdateAsync(sale);
        return Unit.Value;
    }
}

