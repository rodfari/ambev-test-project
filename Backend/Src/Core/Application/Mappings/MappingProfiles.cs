using Application.Dtos;
using Application.Features.Sales.Commands.Create;
using Application.Model;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;
public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<Sale, SaleDto>();
        CreateMap<SaleItem, SaleItemDto>();

        CreateMap<CreateSaleCommand, Sale>()
        .ForMember(dest => dest.Items, opt => opt.Ignore())
        .AfterMap((ori, dest) => {
            foreach (var item in ori.Items)
            {
                dest.AddItem(item.ProductId, item.ProductDescription, item.Quantity, item.UnitPrice);
            }
        });
        CreateMap<CreateSaleItemDto, SaleItem>();

        CreateMap<Sale, SaleReadModel>()
        .ForMember((dest) => dest.Items, opt => opt.Ignore())
        .BeforeMap((ori, dest) => {
            dest.Id = ori.Id.ToString();
        })
        .AfterMap((ori, dest) => {
            dest.Customer = new CustomerInfo
            {
                CustomerId = ori.CustomerId,
                Name = ori.CustomerName
            };
            dest.Branch = new BranchInfo
            {
                BranchId = ori.BranchId,
                Name = ori.BranchName
            };
            
            dest.Items = [.. ori.Items.Select(i => new SaleItemReadModel
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice, 
                Discount = i.Discount,
                Total = i.Total,
            })];
        });

    }
}