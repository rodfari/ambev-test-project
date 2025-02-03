using Application.Dtos;
using Application.Responses;
using MediatR;
namespace Application.Features.Sales.Commands.Update;

public class UpdateSaleCommand : IRequest<TResponse<Unit>>
{
    public Guid Id { get; set; }
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string BranchId { get; set; }
    public string BranchName { get; set; }
    public List<UpdateSaleItemDto> Items { get; set; } = new();
}
