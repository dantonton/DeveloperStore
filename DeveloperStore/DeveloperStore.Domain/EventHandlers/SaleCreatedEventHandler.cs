using DeveloperStore.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DeveloperStore.Domain.EventHandlers
{
    public class SaleCreatedEventHandler(ILogger<SaleCreatedEventHandler> logger) : INotificationHandler<SaleCreatedEvent>
    {
        private readonly ILogger<SaleCreatedEventHandler> _logger = logger;

        public async Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
        {
            var sale = notification.Sale;
            string message = $"Sale created with ID: {sale.Id}, Date: {sale.SaleDate}, Customer: {sale.Customer}, Total: {sale.Total:C}";
            _logger.LogInformation(message);

        }
    }
}
