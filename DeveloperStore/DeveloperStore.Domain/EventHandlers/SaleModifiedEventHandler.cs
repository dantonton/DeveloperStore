using DeveloperStore.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DeveloperStore.Domain.EventHandlers
{
    public class SaleModifiedEventHandler(ILogger<SaleModifiedEventHandler> logger) : INotificationHandler<SaleModifiedEvent>
    {
        private readonly ILogger<SaleModifiedEventHandler> _logger = logger;

        public async Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
        {
            var sale = notification.Sale;
            string message = $"Sale created with ID: {sale.Id}, Date: {sale.SaleDate}, Customer: {sale.Customer}, Old Total: {notification.OldTotal:C}, Total: {sale.Total:C}";
            _logger.LogInformation(message);

        }
    }
}
