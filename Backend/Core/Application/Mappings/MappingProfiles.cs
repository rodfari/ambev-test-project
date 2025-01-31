using Application.Dtos;
using Application.Features.Products.Command.Create;
using Application.Responses.Products;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;
public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<Products, ProductsDto>().ReverseMap();
        CreateMap<Products, CreateProductResponse>().ReverseMap();
        CreateMap<Products, CreateProductCommand>().ReverseMap();
    }
}