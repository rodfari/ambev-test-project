namespace Application.Dtos;

public class SaleDto
{
    public Guid Id { get; set; }
    public DateTime SaleDate { get; set; }
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string BranchId { get; set; }
    public string BranchName { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
    public List<SaleItemDto> Items { get; set; } = new();
}
