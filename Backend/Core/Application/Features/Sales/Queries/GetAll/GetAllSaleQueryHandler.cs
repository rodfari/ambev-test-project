using Application.Dtos;
using AutoMapper;
using Domain.Contracts;
using MediatR;

namespace Application.Features.Sales.Queries;
public class GetAllSaleQueryHandler: IRequestHandler<GetAllSaleQuery, List<SaleDto>>
{
    private readonly ISaleRepository _salesRepository;
    private readonly IMapper _mapper;

    public GetAllSaleQueryHandler(ISaleRepository salesRepository, IMapper mapper)
    {
        _salesRepository = salesRepository;
        _mapper = mapper;
    }

public async Task<List<SaleDto>> Handle(GetAllSaleQuery request, CancellationToken cancellationToken)
    {
        var sales = await _salesRepository.GetAllAsync();
        return _mapper.Map<List<SaleDto>>(sales);
    }
}