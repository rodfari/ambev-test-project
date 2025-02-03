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
            Sale sale = _mapper.Map<Sale>(request);

            var newSale = await _saleRepository.AddAsync(sale);
            SaleReadModel saleReadModel = _mapper.Map<SaleReadModel>(newSale);

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