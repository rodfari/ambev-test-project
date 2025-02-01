using Application.Dtos;
using MediatR;

public record GetSaleByIdQuery(Guid SaleId) : IRequest<SaleDto>;
