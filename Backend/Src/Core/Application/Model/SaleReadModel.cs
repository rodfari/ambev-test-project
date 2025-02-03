namespace Application.Model;

public class SaleReadModel
{
    public string Id { get; set; }
    public DateTime SaleDate { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public CustomerInfo Customer { get; set; } = default!;
    public BranchInfo Branch { get; set; } = default!;
    public decimal TotalAmount { get; set; }
    public List<SaleItemReadModel> Items { get; set; } = new();
    public bool IsCancelled { get; set; }
}
