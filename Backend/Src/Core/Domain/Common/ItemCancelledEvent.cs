namespace Domain.Common;

public record ItemCancelledEvent(Guid SaleId, Guid ProductId) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
