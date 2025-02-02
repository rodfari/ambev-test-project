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

    public CreateSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<TResponse<Guid>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
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