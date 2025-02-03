using Application.Model;
using Application.Repository;
using Application.Responses;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Sales.Commands.Update;
public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, TResponse<Unit>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ISalesReadRepository _salesReadRepository;
    private readonly IMapper _mapper;

    public UpdateSaleCommandHandler(
        ISaleRepository saleRepository, 
        ISalesReadRepository salesReadRepository, 
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _salesReadRepository = salesReadRepository;
        _mapper = mapper;
    }

    public async Task<TResponse<Unit>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var sale = await _saleRepository.GetByIdAsync(request.Id);
            if (sale == null) throw new NotFoundException("Sale not found");

            sale.UpdateCustomer(request.CustomerId, request.CustomerName);
            sale.UpdateBranch(request.BranchId, request.BranchName);
            sale.UpdateItems(request.Items.Select(i => new SaleItem(i.ProductId, i.ProductDescription, i.Quantity, i.UnitPrice)).ToList());

            await _saleRepository.UpdateAsync(sale);

            SaleReadModel saleReadModel = _mapper.Map<SaleReadModel>(sale);
            await _salesReadRepository.UpdateAsync(saleReadModel);

            return new TResponse<Unit>
            {
                Success = true,
                Message = "Sale updated successfully"
            };
        }
        catch (ItemAmountExceededException ex)
        {
            return new TResponse<Unit>
            {
                Success = false,
                Errors = new List<string> { ex.Message }
            };
        }
        catch (MinimumItemRequiredException ex)
        {
            return new TResponse<Unit>
            {
                Success = false,
                Errors = new List<string> { ex.Message }
            };
        }
        catch (NotFoundException ex)
        {
            return new TResponse<Unit>
            {
                Success = false,
                Errors = new List<string> { ex.Message }
            };
        }
        catch (Exception ex)
        {
            return new TResponse<Unit>
            {
                Success = false,
                Errors = new List<string> { ex.Message }
            };
        }
    }

}
