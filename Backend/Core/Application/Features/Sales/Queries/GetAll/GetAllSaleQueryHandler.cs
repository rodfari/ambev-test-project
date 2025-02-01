using Application.Dtos;
using AutoMapper;
using Domain.Contracts;

namespace Application.Features.Sales.Queries;
public class GetAllSaleQueryHandler
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