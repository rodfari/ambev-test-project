using Application.Dtos;
using Application.Responses;
using MediatR;

namespace Application.Features.Sales.Commands.Create;

public class CreateSaleCommand : IRequest<TResponse<Guid>>
{
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string BranchId { get; set; }
    public string BranchName { get; set; }
    public List<CreateSaleItemDto> Items { get; set; } = new();
}
