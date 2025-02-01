namespace Application.Dtos;

public class SaleItemDto
{
    public string ProductId { get; set; }
    public string ProductDescription { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
}
