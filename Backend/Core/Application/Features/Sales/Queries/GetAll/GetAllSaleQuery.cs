using Application.Dtos;
using MediatR;

namespace Application.Features.Sales.Queries;
public record GetAllSaleQuery() : IRequest<List<SaleDto>>;
