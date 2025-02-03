using Application.Repository;
using Application.Responses;
using Domain.Contracts;
using MediatR;

namespace Application.Features.Sales.Commands.Delete;

public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, TResponse<Unit>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ISalesReadRepository _salesReadRepository;
    public DeleteSaleCommandHandler(ISaleRepository saleRepository, ISalesReadRepository salesReadRepository)
    {
        _saleRepository = saleRepository;
        _salesReadRepository = salesReadRepository;
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
        await _salesReadRepository.DeleteAsync(sale.Id.ToString());
        return new TResponse<Unit> { 
            Success = true, 
            Message = "Sale deleted successfully" 
        };
    }
}
