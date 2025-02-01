using Application.Dtos;
using AutoMapper;
using Domain.Entities;

public class SalesMappingProfile : Profile
{
    public SalesMappingProfile()
    {
        CreateMap<Sale, SaleDto>();
        CreateMap<SaleItem, SaleItemDto>();
    }
}
