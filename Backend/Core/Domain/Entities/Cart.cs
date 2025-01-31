namespace Domain.Entities;
public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public List<Products> Items { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice
    {
        get
        {
            return Items.Sum(x => x.Price * this.Quantity);
        }
    }
}