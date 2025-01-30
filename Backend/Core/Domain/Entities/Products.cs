using Core.Domain.Entities;

namespace Domain.Entities;

public class Products: DefaultEntity
{
    public string Title { get; set; }
    public Decimal Price { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }

}

public class ProductRating: DefaultEntity
{
    public int Rating { get; set; }
    public Guid ProductId { get; set; }
    public Products Product { get; set; }
}