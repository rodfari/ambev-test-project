
using Domain.Common;

public record SaleCancelledEvent(Guid SaleId): IDomainEvent
{
    public DateTime OccurredOn => DateTime.Now;
}
