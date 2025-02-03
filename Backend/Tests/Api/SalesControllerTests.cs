using Application.Dtos;
using Application.Features.Sales.Commands.Create;
using Application.Features.Sales.Commands.Update;
using Application.Model;
using Application.Responses;
using Bogus;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;

namespace TestsApi;

public class SalesControllerTests
{

    private readonly SalesController _controller;
    private readonly IMediator _mediator = Substitute.For<IMediator>();

    public SalesControllerTests()
    {
        _controller = new SalesController(_mediator);
    }

    [Fact]
    public async Task GetSaleById_ShouldReturn_OkObjectResult()
    {
        // Arrange
        SaleReadModel sale = new Faker<SaleReadModel>()
            .RuleFor(s => s.Id, f => f.Random.Guid().ToString())
            .RuleFor(s => s.Branch,f =>  new BranchInfo { 
                BranchId = f.Random.Guid().ToString(), 
                Name = f.Company.CompanyName() 
            })
            .RuleFor(s => s.Customer, f => new CustomerInfo { 
                CustomerId = f.Random.Guid().ToString(), 
                Name = f.Person.FullName 
            })
            .RuleFor(s => s.TotalAmount, f => f.Finance.Amount())
            .RuleFor(s => s.Items, f => new List<SaleItemReadModel>{
                new SaleItemReadModel{
                    Total = f.Finance.Amount(),
                    Discount = f.Finance.Amount(),
                    ProductId = f.Random.Guid().ToString(),
                    ProductName = f.Commerce.ProductName(),
                    Quantity = f.Random.Int(1, 10),
                    UnitPrice = f.Finance.Amount()
                }
            })
            .Generate();

        _mediator.Send(Arg.Any<GetSaleByIdQuery>()).Returns(Task.FromResult(new TResponse<SaleReadModel>
        {
            Success = true,
            Data = sale
        }));

        // Act
        var controllerResult = await _controller.GetSaleById(sale.Id.ToString());
        var okResult = controllerResult as OkObjectResult;

        // Assert
        controllerResult.ShouldBeOfType<OkObjectResult>();
        okResult!.StatusCode.ShouldBe(StatusCodes.Status200OK);
        okResult.Value.ShouldBe(sale);
    }

    [Fact]
    public async Task CreateSale_ShouldReturn_CreatedAtAction()
    {
        // Arrange
        var fakeCommand = new Faker<CreateSaleCommand>()
            .RuleFor(c => c.BranchId, f => f.Random.Guid().ToString())
            .RuleFor(c => c.BranchName, f => f.Company.CompanyName())
            .RuleFor(c => c.CustomerId, f => f.Random.Guid().ToString())
            .RuleFor(c => c.CustomerName, f => f.Person.FullName);

        var command = fakeCommand.Generate();

        var saleDto = new SaleDto { Id = Guid.NewGuid(), CustomerId = command.CustomerId };

        _mediator.Send(command).Returns(Task.FromResult(new TResponse<Guid>
        {
            Success = true,
            Data = saleDto.Id
        }));

        // Act
        var controllerResult = await _controller.CreateSale(command);
        var created = controllerResult as CreatedAtActionResult;

        // Assert
        controllerResult.ShouldBeOfType<CreatedAtActionResult>();
        created!.StatusCode.ShouldBe(StatusCodes.Status201Created);
        created.ActionName.ShouldBe("GetSaleById");
    }

    [Fact]
    public async Task PutMethodShouldReturntMessage_SaleUpdated()
    {
        // Arrange
        var fakeCommand = new Faker<UpdateSaleCommand>()
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.BranchId, f => f.Random.Guid().ToString())
            .RuleFor(c => c.BranchName, f => f.Company.CompanyName())
            .RuleFor(c => c.CustomerId, f => f.Random.Guid().ToString())
            .RuleFor(c => c.CustomerName, f => f.Person.FullName);

        var command = fakeCommand.Generate();

        var saleDto = new SaleDto { Id = command.Id, CustomerId = command.CustomerId };

        _mediator.Send(command).Returns(Task.FromResult(new TResponse<Unit>
        {
            Success = true,
            Message = "Sale updated"
        }));

        // Act
        var controllerResult = await _controller.UpdateSale(command);
        var okResult = controllerResult as OkObjectResult;

        // Assert
        controllerResult.ShouldBeOfType<OkObjectResult>();
        okResult!.StatusCode.ShouldBe(StatusCodes.Status200OK);
        okResult.Value.ShouldBe("Sale updated");
    }
}