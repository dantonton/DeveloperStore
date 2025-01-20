using DeveloperStore.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DeveloperStore.Domain.EventHandlers
{
    public class SaleCancelledEventHandler(ILogger<SaleCancelledEventHandler> logger) : INotificationHandler<SaleCancelledEvent>
    {
        private readonly ILogger<SaleCancelledEventHandler> _logger = logger;

        public async Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
        {
            var sale = notification.Sale;
            string message = $"Sale cancelled with ID: {sale.Id}, Date: {sale.SaleDate}, Customer: {sale.Customer}, Total: {sale.Total:C}";
            _logger.LogInformation(message);

        }
    }
}
