using Domain.Entities;
using Domain.Exceptions;

namespace TestesDomain;

public class TestsSales
{
    [Fact]
    public void PurchasesAbove4_IdenticalItems_Have_a_10Percente_Discount()
    {
        // Arrange
        var sales = new Sale();

        // Act
        sales.AddItem("KMFDRG", "Product 1", 5, 10);

        // Assert
        foreach (var item in sales.Items)
        {
            Assert.Equal(45, item.Total);
        }
    }

    [Fact]
    public void PurchasesAbove10_IdenticalItems_Have_a_20Percente_Discount()
    {
        // Arrange
        var sales = new Sale();

        // Act
        sales.AddItem("KMFDRG", "Product 1", 15, 10);

        // Assert
        foreach (var item in sales.Items)
        {
            Assert.Equal(120, item.Total);
        }
    }

    [Fact]
    public void PurchasesAbove20_IdenticalItems_Throws_ItemAmountExceededException()
    {
        // Arrange
        var sales = new Sale();

        // Act
        Action act = () => sales.AddItem("KMFDRG", "Product 1", 25, 10);

        // Assert
        var exception = Assert.Throws<ItemAmountExceededException>(act);
        Assert.Equal("Cannot sell more than 20 of the same item.", exception.Message);
    }

    [Fact]
    public void PuchasesWithLessThan5ItemHasNoDiscount()
    {
        // Arrange
        var sales = new Sale();

        // Act
        sales.AddItem("KMFDRG", "Product 1", 3, 10);

        // Assert
        foreach (var item in sales.Items)
        {
            Assert.Equal(30, item.Total);
        }
    }

    [Fact]
    public void PurchasesWithQuantityLessThan1_Throws_Exception()
    {
        // Arrange
        var sales = new Sale();

        // Act
        Action act = () => sales.AddItem("KMFDRG", "Product 1", 0, 10);

        // Assert
        var exception = Assert.Throws<MinimumItemRequiredException>(act);
        Assert.Equal("Quantity must be at least 1.", exception.Message);
    }
}