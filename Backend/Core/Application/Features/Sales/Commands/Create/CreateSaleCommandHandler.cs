using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.Features.Sales.Commands.Create;
public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public CreateSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = new Sale(request.CustomerId, request.CustomerName, request.BranchId, request.BranchName, DateTime.UtcNow);

        foreach (var item in request.Items)
        {
            sale.AddItem(item.ProductId, item.ProductDescription, item.Quantity, item.UnitPrice);
        }

        await _saleRepository.AddAsync(sale);
        return sale.Id;
    }
}