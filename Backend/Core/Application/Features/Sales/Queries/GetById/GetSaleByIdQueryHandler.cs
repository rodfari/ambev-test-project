using Application.Model;
using Application.Repository;
using Application.Responses;
using AutoMapper;
using MediatR;

public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, TResponse<SaleReadModel>>
{
    private readonly ISalesReadRepository _salesReadRepository;
    private readonly IMapper _mapper;

    public GetSaleByIdQueryHandler(ISalesReadRepository salesReadRepository, IMapper mapper)
    {
        _salesReadRepository = salesReadRepository;
        _mapper = mapper;
    }

    public async Task<TResponse<SaleReadModel>> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {

            var sale = await _salesReadRepository.GetByIdAsync(request.SaleId);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {request.SaleId} not found.");

            return new TResponse<SaleReadModel>
            {
                Success = true,
                Data = sale
            };
        }
        catch(KeyNotFoundException ex)
        {
            return new TResponse<SaleReadModel>
            {
                Success = false,
                Errors = new List<string> { ex.Message }
            };
        }
        catch (Exception ex)
        {
            return new TResponse<SaleReadModel>
            {
                Success = false,
                Errors = new List<string> { ex.Message }
            };
        }
    }
}
