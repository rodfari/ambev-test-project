using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Sales.Commands.Update;
public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, Unit>
{
    private readonly ISaleRepository _saleRepository;

    public UpdateSaleCommandHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<Unit> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.SaleId);
        if (sale == null) throw new NotFoundException("Sale not found");

        sale.UpdateCustomer(request.CustomerId, request.CustomerName);
        sale.UpdateBranch(request.BranchId, request.BranchName);
        sale.UpdateItems(request.Items.Select(i => new SaleItem(i.ProductId, i.ProductDescription, i.Quantity, i.UnitPrice)).ToList());

        await _saleRepository.UpdateAsync(sale);
        return Unit.Value;
    }

}
