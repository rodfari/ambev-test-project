using Core.Domain.Entities;

namespace Domain.Entities;

public class ProductRating: DefaultEntity
{
    public int Rating { get; set; }
    public Guid ProductId { get; set; }
    public Products Product { get; set; }
}