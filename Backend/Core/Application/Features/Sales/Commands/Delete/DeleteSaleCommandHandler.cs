using Domain.Contracts;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Sales.Commands.Delete;

public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, Unit>
{
    private readonly ISaleRepository _saleRepository;

    public DeleteSaleCommandHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<Unit> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.SaleId);
        if (sale == null) throw new NotFoundException("Sale not found");

        await _saleRepository.DeleteAsync(sale.Id);
        return Unit.Value;
    }
}
