using Application.Model;
using Application.Repository;
using AutoMapper;
using MediatR;

namespace Application.Features.Sales.Queries;
public class GetAllSaleQueryHandler : IRequestHandler<GetAllSaleQuery, List<SaleReadModel>>
{
    readonly ISalesReadRepository _salesReadRepository;
    private readonly IMapper _mapper;

    public GetAllSaleQueryHandler(ISalesReadRepository salesReadRepository, IMapper mapper)
    {
        _salesReadRepository = salesReadRepository;
        _mapper = mapper;
    }

    public async Task<List<SaleReadModel>> Handle(GetAllSaleQuery request, CancellationToken cancellationToken)
    {
        var sales = await _salesReadRepository.GetAllAsync(request.Page, request.PageSize);
        return sales;
    }
}