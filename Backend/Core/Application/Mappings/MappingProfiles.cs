using Application.Dtos;
using Application.Features.Sales.Commands.Create;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;
public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<Sale, SaleDto>();
        CreateMap<SaleItem, SaleItemDto>();

        CreateMap<CreateSaleCommand, Sale>();
        CreateMap<CreateSaleItemDto, SaleItem>();
    }
}