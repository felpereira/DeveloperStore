using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Common
{
    // Marker interface for domain events.
    // Inheriting from INotification makes it compatible with MediatR's publishing mechanism.
    public interface IDomainEvent : INotification
    {
    }
}
