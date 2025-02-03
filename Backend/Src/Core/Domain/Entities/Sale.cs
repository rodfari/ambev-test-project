using Domain.Common;
using Domain.Exceptions;

namespace Domain.Entities;

public class Sale
{
    public Guid Id { get; set; }
    public DateTime SaleDate { get; set; }
    public int SaleNumber { get; set; }
    public string CustomerId { get; set; } // External Identity
    public string CustomerName { get; set; } // Denormalized
    public string BranchId { get; set; } // External Identity
    public string BranchName { get; set; } // Denormalized
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
    private readonly List<SaleItem> _items = new();

    public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();


    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void AddItem(
        string productId, 
        string productDescription, 
        int quantity, 
        decimal unitPrice)
    {
        if (quantity > 20) throw new ItemAmountExceededException("Cannot sell more than 20 of the same item.");
        if (quantity < 1) throw new MinimumItemRequiredException("Quantity must be at least 1.");

        decimal discount = GetDiscount(quantity, unitPrice);
        var item = new SaleItem(productId, productDescription, quantity, unitPrice, discount);
        _items.Add(item);
        CalculateTotal();
    }

    private decimal GetDiscount(int quantity, decimal unitPrice)
    {
        if (quantity >= 10) return unitPrice * quantity * 0.2m; // 20% discount
        if (quantity >= 4) return unitPrice * quantity * 0.1m; // 10% discount
        return 0; // No discount
    }

    private void CalculateTotal()
    {
        TotalAmount = _items.Sum(i => i.Total);
    }

    public void CancelSale()
    {
        IsCancelled = true;
        // Raise SaleCancelledEvent (later)
    }

    public void UpdateCustomer(string customerId, string customerName)
    {
        if (string.IsNullOrWhiteSpace(customerId))
            throw new ArgumentException("Customer ID cannot be empty.");

        CustomerId = customerId;
        CustomerName = customerName;
    }

    public void UpdateBranch(string branchId, string branchName)
    {
        if (string.IsNullOrWhiteSpace(branchId))
            throw new ArgumentException("Branch ID cannot be empty.");

        BranchId = branchId;
        BranchName = branchName;
    }


    public void UpdateItems(List<SaleItem> items)
    {
        if (items == null || !items.Any())
            throw new ArgumentException("Sale must have at least one item.");

        _items.Clear();
        _items.AddRange(items);
        CalculateTotal();
    }

    public void Cancel()
    {
        if (IsCancelled)
            throw new InvalidOperationException("Sale is already cancelled.");

        IsCancelled = true;
        AddDomainEvent(new SaleCancelledEvent(Id));
    }

    public void CancelItem(string productId)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot cancel an item from a cancelled sale.");

        var item = _items.FirstOrDefault(i => i.ProductId == productId);

        if (item == null)
            throw new InvalidOperationException("Item not found in sale.");

        item.Cancel();
        CalculateTotal();
    }

}
