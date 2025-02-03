namespace Application.Dtos;

public class UpdateSaleItemDto
{
    public string ProductId { get; set; }
    public string ProductDescription { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
