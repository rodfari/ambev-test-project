using Application.Responses;
using MediatR;

namespace Application.Features.Sales.Commands.Delete;

public class DeleteSaleCommand : IRequest<TResponse<Unit>>
{
    public DeleteSaleCommand()
    {
        
    }
    public DeleteSaleCommand(Guid Id)
    {
        SaleId = Id;
    }
    public Guid SaleId { get; set; }
}
