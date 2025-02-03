using Application.Responses;
using Domain.Contracts;
using MediatR;

namespace Application.Features.Sales.Commands.Delete;

public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, TResponse<Unit>>
{
    private readonly ISaleRepository _saleRepository;

    public DeleteSaleCommandHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<TResponse<Unit>> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.SaleId);
        if (sale == null) {
            return new TResponse<Unit> { 
                Success = false,
                Message = "Sale not found" 
            };
        }

        await _saleRepository.DeleteAsync(sale.Id);
        return new TResponse<Unit> { 
            Success = true,
            Message = "Sale deleted successfully" 
        };
    }
}
