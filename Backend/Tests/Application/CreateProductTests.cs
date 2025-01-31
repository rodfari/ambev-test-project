using Application.Features.Products.Command.Create;
using Application.Mappings;
using Application.Responses.Products;
using AutoFixture;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Moq;
using Shouldly;

namespace TestsApplication;

public class CreateProductTests
{

    [Fact]
    public async Task CreateProductSuccess()
    {
        var mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfiles());
        }).CreateMapper();

        var productRepository = new Mock<IProductRepository>();

        var fixture = new Fixture();
        var request = fixture.Create<CreateProductCommand>();

        productRepository
        .Setup(x => x.AddAsync(It.IsAny<Products>()))
        .ReturnsAsync(new Products(){
            Category = request.Category,
            Description = request.Description,
            Price = request.Price,
            Image = request.Image 
        });

        var handler = new CreateProductCommandHandler(productRepository.Object, mapper);
        var response = await handler.Handle(request, CancellationToken.None);
        response.Success.ShouldBeTrue();
        response.Data.ShouldBeOfType<CreateProductResponse>();
    }
}