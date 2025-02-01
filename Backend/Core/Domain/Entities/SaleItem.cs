namespace Domain.Entities;

public class SaleItem
{
    public Guid Id { get; private set; }
    public string ProductId { get; private set; } // External Identity
    public string ProductDescription { get; private set; } // Denormalized
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Total { get; private set; }
    public bool IsCancelled { get; private set; }
    private SaleItem() { } // For EF Core

    public SaleItem(string productId, string productDescription, int quantity, decimal unitPrice, decimal discount)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        ProductDescription = productDescription;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
        Total = (Quantity * UnitPrice) - Discount;
    }

    public SaleItem(string productId, string productDescription, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        ProductDescription = productDescription;
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
