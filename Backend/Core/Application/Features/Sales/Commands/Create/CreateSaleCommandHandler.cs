using Application.Model;
using Application.Repository;
using Application.Responses;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Sales.Commands.Create;
public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, TResponse<Guid>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ISalesReadRepository _salesReadRepository;

    public CreateSaleCommandHandler(
        ISaleRepository saleRepository, 
        ISalesReadRepository salesReadRepository,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _salesReadRepository = salesReadRepository;
        _mapper = mapper;
    }

    public async Task<TResponse<Guid>> Handle(
        CreateSaleCommand request, 
        CancellationToken cancellationToken)
    {
        try
        {

            var sale = new Sale(
                request.CustomerId,
                request.CustomerName,
                request.BranchId,
                request.BranchName,
                DateTime.UtcNow
            );

            foreach (var item in request.Items)
            {
                sale.AddItem(item.ProductId, item.ProductDescription, item.Quantity, item.UnitPrice);
            }

            await _saleRepository.AddAsync(sale);
            SaleReadModel saleReadModel = new SaleReadModel
            {
                Id = sale.Id.ToString(),
                // SaleNumber = sale.number,
                SaleDate = sale.SaleDate,
                Customer = new CustomerInfo
                {
                    CustomerId = sale.CustomerId,
                    Name = sale.CustomerName
                },
                Branch = new BranchInfo
                {
                    BranchId = sale.BranchId,
                    Name = sale.BranchName
                },
                TotalAmount = sale.TotalAmount,
                //Items = _mapper.Map<List<SaleItemReadModel>>(sale.Items),
                Items = [.. sale.Items.Select(i => new SaleItemReadModel
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                })],
                IsCancelled = sale.IsCancelled
                
                
            };

            await _salesReadRepository.InsertAsync(saleReadModel);            

            return new TResponse<Guid>
            {
                Success = true,
                Data = sale.Id
            };
        }
        catch (ItemAmountExceededException ex)
        {
            return new TResponse<Guid>
            {
                Success = false,
                Data = Guid.Empty,
                Errors = new List<string> { ex.Message }
            };
        }
        catch (MinimumItemRequiredException ex)
        {
            return new TResponse<Guid>
            {
                Success = false,
                Data = Guid.Empty,
                Errors = new List<string> { ex.Message }

            };
        }
        catch (Exception ex)
        {
            return new TResponse<Guid>
            {
                Success = false,
                Data = Guid.Empty,
                Errors = new List<string> { ex.Message }
            };
        }
    }
}