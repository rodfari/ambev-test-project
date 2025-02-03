using MediatR;

namespace Application.Features.SalesItems.Command;
public class CancelSaleItemCommand : IRequest<Unit>
{
    public Guid SaleId { get; set; }
    public string ProductId { get; set; }
}