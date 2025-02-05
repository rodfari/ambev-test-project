namespace Domain.Entities;

public class SaleItem
{
    public Guid Id { get; set; }
    public string ProductId { get; set; } // External Identity
    public string ProductName { get; set; } // Denormalized
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public bool IsCancelled { get; set; }
    public Guid SaleId { get; set; }
    // public Sale Sale { get; set; }

    private SaleItem() { } // For EF Core

    public SaleItem(string productId, string productName, int quantity, decimal unitPrice, decimal discount)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
        Total = (Quantity * UnitPrice) - Discount;
    }

    public SaleItem(string productId, string productName, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Total = (Quantity * UnitPrice) - Discount;
    }

    public void Cancel()
    {
        if (IsCancelled)
            throw new InvalidOperationException("Item is already cancelled.");

        IsCancelled = true;
        Total = 0; // Set total to zero since the item is no longer part of the sale.
    }

}
