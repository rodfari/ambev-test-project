using Application.Dtos;
using Application.Model;
using Application.Responses;
using MediatR;

public record GetSaleByIdQuery(string SaleId) : IRequest<TResponse<SaleReadModel>>;
