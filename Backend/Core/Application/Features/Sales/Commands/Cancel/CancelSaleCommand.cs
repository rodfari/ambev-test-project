using MediatR;

namespace Application.Features.Sales.Commands.Cancel;
public class CancelSaleCommand: IRequest<Unit>
{
    public Guid SaleId { get; set; }
    public bool IsCancelled { get; set; }
    
}