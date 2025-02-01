using Application.Dtos;
using Application.Features.Products.Command.Update;
using AutoFixture;
using Domain.Entities;

namespace TestsApplication.Fixtures;

public class UpdateProductFixture
{
    public static UpdateProductCommand Request(){
        var fixture = new Fixture();
        fixture.Customize<UpdateProductCommand>(
            x => x.With(p => p.Id, 1)
            .With(p => p.Price, 10)
            .With(r => r.Rating, new RatingDto{ Rate = 5, Count = 5 }));

        var request = fixture.Create<UpdateProductCommand>();
        return request;
    }

    public static Products Product(){
        var fixture = new Fixture();
        var product = fixture.Build<Products>()
            .With(p => p.Id, 1)
            .With(p => p.Price, 10)
            .With(r => r.Rating, new Domain.ValueObjects.Rating{ Rate = 4, Count = 1 })
            .Create();
        return product;
    }
}