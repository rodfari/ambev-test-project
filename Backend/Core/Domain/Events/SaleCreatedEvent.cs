namespace Domain.Events;
public class SaleCreatedEvent
{
    public Guid SaleId { get; }
    public DateTime SaleDate { get; }
    public decimal TotalAmount { get; }

    public SaleCreatedEvent(Guid saleId, DateTime saleDate, decimal totalAmount)
    {
        SaleId = saleId;
        SaleDate = saleDate;
        TotalAmount = totalAmount;
    }
}
