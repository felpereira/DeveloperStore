using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events
{
    // Handler for the SaleCreatedEvent.
    public class SaleCreatedEventHandler : INotificationHandler<SaleCreatedEvent>
    {
        private readonly ILogger<SaleCreatedEventHandler> _logger;

        public SaleCreatedEventHandler(ILogger<SaleCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: SaleCreatedEvent published for SaleId: {SaleId}", notification.SaleId);

            // Here you could, for example:
            // - Send a confirmation email
            // - Update an inventory service
            // - Notify a shipping department

            return Task.CompletedTask;
        }
    }
}
