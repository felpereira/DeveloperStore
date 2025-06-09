using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    // Event that is raised when a new sale is created.
    public class SaleCreatedEvent : IDomainEvent
    {
        public SaleCreatedEvent(Guid saleId)
        {
            SaleId = saleId;
        }

        public Guid SaleId { get; }
    }
}
