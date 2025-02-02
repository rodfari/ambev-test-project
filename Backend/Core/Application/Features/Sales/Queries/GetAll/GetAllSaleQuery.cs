using Application.Dtos;
using Application.Model;
using MediatR;

namespace Application.Features.Sales.Queries;
public class GetAllSaleQuery() : IRequest<List<SaleReadModel>>{
    public int Page { get; set; }
    public int PageSize { get; set; }
}
