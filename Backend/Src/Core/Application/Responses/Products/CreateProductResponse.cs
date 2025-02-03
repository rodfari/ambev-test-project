namespace Application.Responses.Products;
public class CreateProductResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }
}
