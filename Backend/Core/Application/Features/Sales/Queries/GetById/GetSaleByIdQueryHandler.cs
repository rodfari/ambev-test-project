using Application.Dtos;
using AutoMapper;
using Domain.Contracts;
using MediatR;

public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, SaleDto>
{
    private readonly ISaleRepository _salesRepository;
    private readonly IMapper _mapper;

    public GetSaleByIdQueryHandler(ISaleRepository salesRepository, IMapper mapper)
    {
        _salesRepository = salesRepository;
        _mapper = mapper;
    }

    public async Task<SaleDto> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await _salesRepository.GetByIdAsync(request.SaleId);
            

        if (sale == null)
            throw new KeyNotFoundException($"Sale with ID {request.SaleId} not found.");

        return _mapper.Map<SaleDto>(sale);
    }
}
