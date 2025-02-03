namespace Domain.Common;

public record SaleCancelledEvent(Guid SaleId) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
