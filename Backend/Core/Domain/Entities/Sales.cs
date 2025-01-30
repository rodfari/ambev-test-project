namespace Domain.Entities;

public class Sale
{
    public Guid Id { get; private set; }
    public string SaleNumber { get; private set; }
    public DateTime SaleDate { get; private set; }
    public CustomerReference Customer { get; private set; }
    public BranchReference Branch { get; private set; }
    public List<SaleItem> Items { get; private set; }
    public bool IsCancelled { get; private set; }

    public Sale(string saleNumber, DateTime saleDate, CustomerReference customer, BranchReference branch)
    {
        Id = Guid.NewGuid();
        SaleNumber = saleNumber;
        SaleDate = saleDate;
        Customer = customer;
        Branch = branch;
        Items = new List<SaleItem>();
        IsCancelled = false;
    }

    public void AddItem(ProductReference product, int quantity, decimal unitPrice)
    {
        if (quantity > 20) throw new Exception("Cannot sell more than 20 identical items");

        decimal discount = DiscountCalculator.Calculate(quantity, unitPrice);
        decimal totalAmount = (unitPrice * quantity) - discount;

        Items.Add(new SaleItem(product, quantity, unitPrice, discount, totalAmount));
    }

    public void CancelSale() => IsCancelled = true;
}

public class CustomerReference
{
    public string Id { get; private set; }
    public string Name { get; private set; }

    public CustomerReference(string id, string name)
    {
        Id = id;
        Name = name;
    }
}

public class BranchReference
{
    public string Id { get; private set; }
    public string Location { get; private set; }

    public BranchReference(string id, string location)
    {
        Id = id;
        Location = location;
    }
}

public class ProductReference
{
    public string Id { get; private set; }
    public string Name { get; private set; }

    public ProductReference(string id, string name)
    {
        Id = id;
        Name = name;
    }
}




public class SaleItem
{
    public ProductReference Product { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public decimal TotalAmount { get; private set; }

    public SaleItem(ProductReference product, int quantity, decimal unitPrice, decimal discount, decimal totalAmount)
    {
        Product = product;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
        TotalAmount = totalAmount;
    }
}


public static class DiscountCalculator
{
    public static decimal Calculate(int quantity, decimal unitPrice)
    {
        if (quantity < 4) return 0;
        if (quantity >= 10 && quantity <= 20) return unitPrice * quantity * 0.20m;
        if (quantity >= 4) return unitPrice * quantity * 0.10m;
        return 0;
    }
}
