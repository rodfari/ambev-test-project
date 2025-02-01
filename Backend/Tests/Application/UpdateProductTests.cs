using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Products.Command.Update;
using Application.Mappings;
using AutoFixture;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Moq;
using Xunit;

namespace Application;
public class UpdateProductTests
{
    [Fact]
    public void UpdateProductSucess()
    {
        // Arrange
        var mapper = new MapperConfiguration(cfg =>{
            cfg.AddProfile(new MappingProfiles());
        }).CreateMapper();

        var repository = new Mock<IProductRepository>();

        var fixture = new Fixture();
        
        fixture.Customize<UpdateProductCommand>(x => x.With(p => p.Id, 1));

        var request = fixture.Create<UpdateProductCommand>();


        repository.Setup(x => x.UpdateAsync(It.IsAny<Products>()))
            .ReturnsAsync(new Products());
        repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Products());
        
        var handler = new UpdateProductCommandHandler(repository.Object, mapper);

        // Act
        //var result = handler.Handle

        // Assert
        //Assert.True(result.IsValid);
    }
}